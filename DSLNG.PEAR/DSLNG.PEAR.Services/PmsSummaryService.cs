using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.PmsSummary;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using System.Data.Entity;
using NCalc;

namespace DSLNG.PEAR.Services
{
    public class PmsSummaryService : BaseService, IPmsSummaryService
    {
        public PmsSummaryService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetPmsSummaryResponse GetPmsSummary(GetPmsSummaryRequest request)
        {
            var response = new GetPmsSummaryResponse();
            try
            {
                //var xxx = DataContext.PmsSummaries.Include(x => x.PmsSummaryScoringIndicators.Select(a => a.)).ToList();
                var pmsSummary = DataContext.PmsSummaries
                .Include("PmsSummaryScoringIndicators")
                .Include("PmsSummaryScoringIndicators.ScoreIndicators")
                .Include("PmsConfigs.Pillar")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Measurement")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiAchievements")
                .Include("PmsConfigs.PmsConfigDetailsList.Kpi.KpiTargets")
                .Include("PmsConfigs.PmsConfigDetailsList.ScoreIndicators")
                .First(x => x.IsActive && x.Year == request.Year);

                var pillarScoringIndicators =
                    pmsSummary.PmsSummaryScoringIndicators.Where(x => x.Type == PmsSummaryScoringIndicatorType.Pillar)
                              .ToList();

                var totalScoreScoringIndicators =
                    pmsSummary.PmsSummaryScoringIndicators.Where(x => x.Type == PmsSummaryScoringIndicatorType.TotalScore)
                              .ToList();

                foreach (var pmsConfig in pmsSummary.PmsConfigs)
                {
                    foreach (var pmsConfigDetails in pmsConfig.PmsConfigDetailsList)
                    {
                        var kpiData = new GetPmsSummaryResponse.KpiData();
                        kpiData.Id = pmsConfigDetails.Id;
                        kpiData.Pillar = pmsConfig.Pillar.Name;
                        kpiData.Kpi = pmsConfigDetails.Kpi.Name;
                        kpiData.Unit = pmsConfigDetails.Kpi.Measurement.Name;
                        kpiData.Weight = pmsConfigDetails.Weight;
                        kpiData.PillarOrder = pmsConfigDetails.Kpi.Pillar.Order;
                        kpiData.KpiOrder = pmsConfigDetails.Kpi.Order;

                        #region KPI Achievement

                        var kpiAchievementYearly = pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly);
                        if (kpiAchievementYearly != null && kpiAchievementYearly.Value != null)
                            kpiData.ActualYearly = kpiAchievementYearly.Value.Value;


                        var kpiAchievementMonthly =
                            pmsConfigDetails.Kpi.KpiAchievements.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month);
                        if (kpiAchievementMonthly != null && kpiAchievementMonthly.Value.HasValue)
                            kpiData.ActualMonthly = kpiAchievementMonthly.Value.Value;


                        var kpiAchievementYtd = pmsConfigDetails.Kpi.KpiAchievements.Where(
                            x => x.PeriodeType == PeriodeType.Monthly && (x.Periode.Month >= 1 && x.Periode.Month <= request.Month)).ToList();
                        if (kpiAchievementYtd.Count > 0) kpiData.ActualYtd = 0;
                        foreach (var achievementYtd in kpiAchievementYtd)
                        {
                            if (achievementYtd.Value.HasValue)
                                kpiData.ActualYtd += achievementYtd.Value;
                        }

                        #endregion

                        #region KPI Target

                        var kpiTargetYearly = pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly);
                        if (kpiTargetYearly != null && kpiTargetYearly.Value != null)
                            kpiData.TargetYearly = kpiTargetYearly.Value.Value;


                        var kpiTargetMonthly =
                            pmsConfigDetails.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly && x.Periode.Month == request.Month);
                        if (kpiTargetMonthly != null && kpiTargetMonthly.Value.HasValue)
                            kpiData.TargetMonthly = kpiTargetMonthly.Value.Value;


                        var kpiTargetYtd = pmsConfigDetails.Kpi.KpiTargets.Where(
                            x => x.PeriodeType == PeriodeType.Monthly && (x.Periode.Month >= 1 && x.Periode.Month <= request.Month)).ToList();
                        if (kpiTargetYtd.Count > 0) kpiData.TargetYtd = 0;
                        foreach (var targetYtd in kpiTargetYtd)
                        {
                            if (targetYtd.Value.HasValue)
                                kpiData.TargetYtd += targetYtd.Value;
                        }

                        #endregion

                        #region Score
                        if (kpiData.ActualYtd.HasValue && kpiData.TargetYtd.HasValue)
                        {
                            var indexYtd = (kpiData.ActualYtd.Value / kpiData.TargetYtd.Value);

                            switch (pmsConfigDetails.ScoringType)
                            {
                                case ScoringType.Positive:
                                    kpiData.Score = pmsConfigDetails.Weight * indexYtd;
                                    break;
                                case ScoringType.Negative:
                                    if (indexYtd == 0)
                                    {
                                        response.IsSuccess = false;
                                        response.Message =
                                            string.Format(
                                                @"KPI {0} memiliki nilai index YTD 0 dengan Nilai Scoring Type negative yang mengakibatkan terjadinya nilai infinity", pmsConfigDetails.Kpi.Name);
                                        return response;
                                    }
                                    kpiData.Score = pmsConfigDetails.Weight / indexYtd;
                                    break;
                                case ScoringType.Boolean:
                                    bool isMoreThanZero = false;
                                    var kpiAchievement = pmsConfigDetails.Kpi.KpiAchievements.Where(x => x.Value.HasValue).ToList();
                                    bool isNull = kpiAchievement.Count == 0;
                                    foreach (var achievement in kpiAchievement)
                                    {
                                        if (achievement.Value > 0)
                                            isMoreThanZero = true;
                                    }

                                    if (!isNull)
                                    {
                                        kpiData.Score = isMoreThanZero ? 0 : Double.Parse(kpiData.Weight.ToString());
                                    }

                                    break;
                            }

                        }

                        #endregion

                        kpiData.KpiColor = GetKpiColor(kpiData.Score, pmsConfigDetails.ScoreIndicators);

                        response.KpiDatas.Add(kpiData);
                    }
                }

                response.KpiDatas = SetPillarAndTotalScoreColor(response.KpiDatas, pillarScoringIndicators, totalScoreScoringIndicators);
                response.IsSuccess = true;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        private string GetKpiColor(double? score, IEnumerable<ScoreIndicator> scoreIndicators)
        {
            if (score.HasValue)
            {
                foreach (var scoreIndicator in scoreIndicators)
                {
                    Expression e = new Expression(scoreIndicator.Expression.Replace("x", score.ToString()));
                    bool isPassed = (bool)e.Evaluate();
                    if (isPassed)
                    {
                        return scoreIndicator.Color;
                    }
                }
            }

            return "grey";
        }
        
        private IList<GetPmsSummaryResponse.KpiData> SetPillarAndTotalScoreColor(IList<GetPmsSummaryResponse.KpiData> kpiDatas, List<PmsSummaryScoringIndicator> pillarScoringIndicators, List<PmsSummaryScoringIndicator> totalScoreScoringIndicators)
        {
            //var totalPillar = kpiDatas.GroupBy(x => x.Pillar).Select(x => x.ToList()).ToList();
            //IDictionary<string, int> totalPillar = kpiDatas.GroupBy(x => x.Pillar).Select(x => x.Sum(y => y.Score)).ToList();
            var totalPillar =
                kpiDatas.GroupBy(x => x.Pillar).ToDictionary(x => x.Key, x => x.Sum(y => y.Score));

            /*foreach (var tp in totalPillar)
            {
                kpiDatas.Where(x => x.Pillar == tp.Key).ToList();
            }*/

            return kpiDatas;
        }
    }
}
