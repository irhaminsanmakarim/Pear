

using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DSLNG.PEAR.Services
{
    public class ArtifactService : BaseService, IArtifactService
    {
        public ArtifactService(IDataContext dataContext)
            : base(dataContext)
        {
        }


        //public GetSeriesResponse GetSeries(GetSeriesRequest request)
        //{
        //    var seriesResponse = new List<GetSeriesResponse.SeriesResponse>();
        //    var seriesType = "single-stack";
        //    if (request.SeriesList.Count > 1)
        //    {
        //        if (request.SeriesList.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null)
        //        {
        //            seriesType = "multi-stacks-grouped";
        //        }
        //    }
        //    else
        //    {
        //        if (request.SeriesList.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null)
        //        {
        //            seriesType = "multi-stack";
        //        }
        //    }
        //    switch (request.ValueAxis)
        //    {
        //        case ValueAxis.KpiTarget:
        //            {
        //                seriesResponse = this._getKpiTargetSeries(request.SeriesList, request.PeriodeType, dateTimePeriodes, seriesType);
        //            }
        //            break;
        //        case ValueAxis.KpiActual:
        //            {
        //                foreach (var series in request.SeriesList)
        //                {

        //                    if (series.Stacks.Count == 0)
        //                    {
        //                        var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
        //                          x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == series.KpiId)
        //                          .OrderBy(x => x.Periode);
        //                        var aSeries = new GetSeriesResponse.SeriesResponse
        //                        {
        //                            name = series.Label
        //                        };
        //                        foreach (var target in kpiTargets)
        //                        {
        //                            aSeries.data.Add(target.Value.Value);
        //                        }
        //                        seriesResponse.Add(aSeries);
        //                    }
        //                    else
        //                    {
        //                        foreach (var stack in series.Stacks)
        //                        {
        //                            var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
        //                          x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == stack.KpiId)
        //                          .OrderBy(x => x.Periode);
        //                            if (seriesType == "multi-stacks-grouped")
        //                            {
        //                                var aSeries = new GetSeriesResponse.SeriesResponse
        //                                {
        //                                    name = stack.Label,
        //                                    stack = series.Label
        //                                };
        //                                foreach (var target in kpiTargets)
        //                                {
        //                                    aSeries.data.Add(target.Value.Value);
        //                                }
        //                                seriesResponse.Add(aSeries);
        //                            }
        //                            else
        //                            {
        //                                var aSeries = new GetSeriesResponse.SeriesResponse
        //                                {
        //                                    name = series.Label
        //                                };
        //                                foreach (var target in kpiTargets)
        //                                {
        //                                    aSeries.data.Add(target.Value.Value);
        //                                }
        //                                seriesResponse.Add(aSeries);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            break;
        //        default:
        //            break;
        //    }

        //    return new GetSeriesResponse
        //    {
        //        Series = seriesResponse
        //    };

        //}

        public GetChartDataResponse GetChartData(GetChartDataRequest request)
        {
            var response = new GetChartDataResponse();
            IList<DateTime> dateTimePeriodes = new List<DateTime>();
            response.Periodes = this._getPeriodes(request.PeriodeType, request.RangeFilter, request.Start, request.End, out dateTimePeriodes);
            IList<GetChartDataResponse.SeriesResponse> seriesResponse = new List<GetChartDataResponse.SeriesResponse>();
            var seriesType = "single-stack";
            if (request.SeriesList.Count > 1)
            {
                if (request.SeriesList.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null)
                {
                    seriesType = "multi-stacks-grouped";
                }
            }
            else
            {
                if (request.SeriesList.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null)
                {
                    seriesType = "multi-stack";
                }
            }
            switch (request.ValueAxis)
            {
                case ValueAxis.KpiTarget:
                    seriesResponse = this._getKpiTargetSeries(request.SeriesList, request.PeriodeType, dateTimePeriodes, seriesType);
                    break;
                case ValueAxis.KpiActual:
                    seriesResponse = this._getKpiActualSeries(request.SeriesList, request.PeriodeType, dateTimePeriodes, seriesType);
                    break;
            }
            response.SeriesType = seriesType;
            response.Series = seriesResponse;
            return response;
        }

        private string[] _getPeriodes(PeriodeType periodeType, RangeFilter rangeFilter, DateTime? Start, DateTime? End, out IList<DateTime> dateTimePeriodes)
        {
            var periodes = new List<string>();
            dateTimePeriodes = new List<DateTime>();
            switch (periodeType)
            {
                case PeriodeType.Hourly:
                    var hourlyFormat = "MM/DD/yyyy hh:mm A";
                    switch (rangeFilter)
                    {
                        case RangeFilter.CurrentHour:
                            var currentHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
                            dateTimePeriodes.Add(currentHour);
                            periodes.Add(currentHour.ToString(hourlyFormat));
                            break;
                        case RangeFilter.CurrentDay:
                            var startHour = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            periodes.Add(startHour.ToString(hourlyFormat));
                            dateTimePeriodes.Add(startHour);
                            for (double i = 1; i < 24; i++)
                            {
                                startHour = startHour.AddHours(1);
                                periodes.Add(startHour.ToString(hourlyFormat));
                                dateTimePeriodes.Add(startHour);
                            }
                            break;
                        default:
                            while (Start.Value <= End.Value)
                            {
                                periodes.Add(End.Value.ToString(hourlyFormat));
                                dateTimePeriodes.Add(Start.Value);
                                Start = Start.Value.AddHours(1);
                            }
                            break;
                    }
                    break;
                case PeriodeType.Daily:
                    var dailyFormat = "MM/DD/yyyy";
                    switch (rangeFilter)
                    {
                        case RangeFilter.CurrentDay:
                            var currentDay = DateTime.Now.Date;
                            periodes.Add(currentDay.ToString(dailyFormat));
                            dateTimePeriodes.Add(currentDay);
                            break;
                        case RangeFilter.CurrentMonth:
                            var currentMonth = DateTime.Now.Month;
                            var startDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            while (currentMonth == startDay.Month)
                            {
                                periodes.Add(startDay.ToString(dailyFormat));
                                dateTimePeriodes.Add(startDay);
                                startDay = startDay.AddDays(1);
                            }
                            break;
                        default:
                            while (Start.Value <= End.Value)
                            {
                                periodes.Add(End.Value.ToString(dailyFormat));
                                dateTimePeriodes.Add(Start.Value);
                                Start = Start.Value.AddDays(1);
                            }
                            break;

                    }
                    break;
                case PeriodeType.Monthly:
                    var monthlyFormat = "MM/yyyy";
                    switch (rangeFilter)
                    {
                        case RangeFilter.CurrentMonth:
                            var currentMonth = DateTime.Now.Date;
                            dateTimePeriodes.Add(currentMonth);
                            periodes.Add(currentMonth.ToString(monthlyFormat));
                            break;
                        case RangeFilter.CurrentYear:
                            var currentYear = DateTime.Now.Year;
                            var startMonth = new DateTime(DateTime.Now.Year, 1, 1);

                            while (currentYear == startMonth.Year)
                            {
                                periodes.Add(startMonth.ToString(monthlyFormat));
                                dateTimePeriodes.Add(startMonth);
                                startMonth = startMonth.AddMonths(1);
                            }
                            break;
                        default:
                            while (Start.Value <= End.Value)
                            {
                                dateTimePeriodes.Add(Start.Value);
                                periodes.Add(End.Value.ToString(monthlyFormat));
                                Start = Start.Value.AddMonths(1);
                            }
                            break;
                    }
                    break;
                default:
                    var yearlyFormat = "yyyy";
                    switch (rangeFilter)
                    {
                        case RangeFilter.CurrentYear:
                            periodes.Add(DateTime.Now.Year.ToString());
                            dateTimePeriodes.Add(new DateTime(DateTime.Now.Year, 1, 1));
                            break;
                        default:
                            while (Start.Value <= End.Value)
                            {
                                periodes.Add(End.Value.ToString(yearlyFormat));
                                dateTimePeriodes.Add(Start.Value);
                                Start = Start.Value.AddYears(1);
                            }
                            break;
                    }
                    break;
            }

            return periodes.ToArray();
        }

        private IList<GetChartDataResponse.SeriesResponse> _getKpiTargetSeries(IList<GetChartDataRequest.Series> configSeries, PeriodeType periodeType, IList<DateTime> dateTimePeriodes, string seriesType)
        {
            var seriesResponse = new List<GetChartDataResponse.SeriesResponse>();
            var start = dateTimePeriodes[0];
            var end = dateTimePeriodes[dateTimePeriodes.Count - 1];
            foreach (var series in configSeries)
            {

                if (series.Stacks.Count == 0)
                {
                    var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == periodeType &&
                      x.Periode >= start && x.Periode <= end && x.Kpi.Id == series.KpiId)
                      .OrderBy(x => x.Periode).ToList();
                    var aSeries = new GetChartDataResponse.SeriesResponse
                    {
                        name = series.Label
                    };
                    foreach (var periode in dateTimePeriodes)
                    {
                        var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                        if (target == null || !target.Value.HasValue)
                        {
                            aSeries.data.Add(0);
                        }
                        else
                        {
                            aSeries.data.Add(target.Value.Value);
                        }
                    }
                    seriesResponse.Add(aSeries);
                }
                else
                {
                    foreach (var stack in series.Stacks)
                    {
                        var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == periodeType &&
                        x.Periode >= start && x.Periode <= end && x.Kpi.Id == stack.KpiId)
                        .OrderBy(x => x.Periode).ToList();
                        if (seriesType == "multi-stacks-grouped")
                        {
                            var aSeries = new GetChartDataResponse.SeriesResponse
                            {
                                name = stack.Label,
                                stack = series.Label
                            };
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.data.Add(0);
                                }
                                else
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                        else
                        {

                            var aSeries = new GetChartDataResponse.SeriesResponse
                            {
                                name = series.Label
                            };
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.data.Add(0);
                                }
                                else
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                    }
                }
            }
            return seriesResponse;
        }

        private IList<GetChartDataResponse.SeriesResponse> _getKpiActualSeries(IList<GetChartDataRequest.Series> configSeries, PeriodeType periodeType, IList<DateTime> dateTimePeriodes, string seriesType)
        {
            var seriesResponse = new List<GetChartDataResponse.SeriesResponse>();
            foreach (var series in configSeries)
            {

                if (series.Stacks.Count == 0)
                {
                    var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == periodeType &&
                      x.Periode >= dateTimePeriodes[0] && x.Periode <= dateTimePeriodes[dateTimePeriodes.Count - 1] && x.Kpi.Id == series.KpiId)
                      .OrderBy(x => x.Periode).ToList();
                    var aSeries = new GetChartDataResponse.SeriesResponse
                    {
                        name = series.Label
                    };
                    foreach (var periode in dateTimePeriodes)
                    {
                        var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                        if (target == null || !target.Value.HasValue)
                        {
                            aSeries.data.Add(0);
                        }
                        else
                        {
                            aSeries.data.Add(target.Value.Value);
                        }
                    }
                    seriesResponse.Add(aSeries);
                }
                else
                {
                    foreach (var stack in series.Stacks)
                    {
                        var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == periodeType &&
                        x.Periode >= dateTimePeriodes[0] && x.Periode <= dateTimePeriodes[dateTimePeriodes.Count - 1] && x.Kpi.Id == stack.KpiId)
                        .OrderBy(x => x.Periode).ToList();
                        if (seriesType == "multi-stacks-grouped")
                        {
                            var aSeries = new GetChartDataResponse.SeriesResponse
                            {
                                name = stack.Label,
                                stack = series.Label
                            };
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.data.Add(0);
                                }
                                else
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                        else
                        {

                            var aSeries = new GetChartDataResponse.SeriesResponse
                            {
                                name = series.Label
                            };
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.data.Add(0);
                                }
                                else
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                    }
                }
            }
            return seriesResponse;
        }
    }
}
