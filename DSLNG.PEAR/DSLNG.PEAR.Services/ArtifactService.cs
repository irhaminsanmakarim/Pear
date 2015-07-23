

using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;
using System;
using System.Collections.Generic;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity;

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

        public GetSpeedometerChartDataResponse GetSpeedometerChartData(GetSpeedometerChartDataRequest request)
        {
            var response = new GetSpeedometerChartDataResponse();

            var kpi = DataContext.Kpis.Where(x => x.Id == request.Series.KpiId).First();
            IList<DateTime> dateTimePeriodes = new List<DateTime>();
            this._getPeriodes(request.PeriodeType, request.RangeFilter, request.Start, request.End, out dateTimePeriodes);
            var start = dateTimePeriodes[0];
            var end = dateTimePeriodes[dateTimePeriodes.Count - 1];

            switch (kpi.YtdFormula)
            {
                case YtdFormula.Sum:
                    switch (request.ValueAxis)
                    {
                        case ValueAxis.KpiTarget:
                            response.Series = new GetSpeedometerChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Sum(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                        case ValueAxis.KpiActual:
                            response.Series = new GetSpeedometerChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Sum(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                    }
                    break;
                case YtdFormula.Average:
                    switch (request.ValueAxis)
                    {
                        case ValueAxis.KpiTarget:
                            response.Series = new GetSpeedometerChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Average(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                        case ValueAxis.KpiActual:
                            response.Series = new GetSpeedometerChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Average(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                    }
                    break;
            }
            foreach (var plot in request.PlotBands)
            {
                response.PlotBands.Add(new GetSpeedometerChartDataResponse.PlotBandResponse
                {
                    from = plot.From,
                    to = plot.To,
                    color = plot.Color
                });
            }
            return response;
        }

        public GetTrafficLightChartDataResponse GetTrafficLightChartData(GetTrafficLightChartDataRequest request)
        {
            var response = new GetTrafficLightChartDataResponse();

            var kpi = DataContext.Kpis.Where(x => x.Id == request.Series.KpiId).First();
            IList<DateTime> dateTimePeriodes = new List<DateTime>();
            this._getPeriodes(request.PeriodeType, request.RangeFilter, request.Start, request.End, out dateTimePeriodes);
            var start = dateTimePeriodes[0];
            var end = dateTimePeriodes[dateTimePeriodes.Count - 1];

            switch (kpi.YtdFormula)
            {
                case YtdFormula.Sum:
                    switch (request.ValueAxis)
                    {
                        case ValueAxis.KpiTarget:
                            response.Series = new GetTrafficLightChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Sum(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                        case ValueAxis.KpiActual:
                            response.Series = new GetTrafficLightChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Sum(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                    }
                    break;
                case YtdFormula.Average:
                    switch (request.ValueAxis)
                    {
                        case ValueAxis.KpiTarget:
                            response.Series = new GetTrafficLightChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Average(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                        case ValueAxis.KpiActual:
                            response.Series = new GetTrafficLightChartDataResponse.SeriesResponse
                            {
                                name = request.Series.Label,
                                data = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                x.Periode >= start && x.Periode <= end && x.Kpi.Id == request.Series.KpiId)
                                .GroupBy(x => x.Kpi.Id)
                                .Select(x => x.Average(y => y.Value).Value).FirstOrDefault()
                            };
                            break;
                    }
                    break;
            }
            foreach (var plot in request.PlotBands)
            {
                response.PlotBands.Add(new GetTrafficLightChartDataResponse.PlotBandResponse
                {
                    from = plot.From,
                    to = plot.To,
                    color = plot.Color,
                    label = plot.Label
                });
            }
            return response;
        }

        public GetCartesianChartDataResponse GetChartData(GetCartesianChartDataRequest request)
        {
            var response = new GetCartesianChartDataResponse();
            IList<DateTime> dateTimePeriodes = new List<DateTime>();
            response.Periodes = this._getPeriodes(request.PeriodeType, request.RangeFilter, request.Start, request.End, out dateTimePeriodes);
            IList<GetCartesianChartDataResponse.SeriesResponse> seriesResponse = new List<GetCartesianChartDataResponse.SeriesResponse>();
            var seriesType = "single-stack";
            if (request.Series.Count == 1 && (request.GraphicType == "baraccumulative" || request.GraphicType == "barachievement"))
            {
                seriesType = "multi-stacks";
            }
            else if (request.Series.Count > 1)
            {
                if (request.Series.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null || request.GraphicType == "baraccumulative" || request.GraphicType == "barachievement")
                {
                    seriesType = "multi-stacks-grouped";
                }
            }
            else
            {
                if (request.Series.Where(x => x.Stacks.Count > 0).FirstOrDefault() != null || request.GraphicType == "baraccumulative")
                {
                    seriesType = "multi-stack";
                }
            }
            switch (request.ValueAxis)
            {
                case ValueAxis.KpiTarget:
                    seriesResponse = this._getKpiTargetSeries(request.Series, request.PeriodeType, dateTimePeriodes, seriesType, request.RangeFilter, request.GraphicType);
                    break;
                case ValueAxis.KpiActual:
                    seriesResponse = this._getKpiActualSeries(request.Series, request.PeriodeType, dateTimePeriodes, seriesType, request.RangeFilter, request.GraphicType);
                    break;
                case ValueAxis.Custom:
                    var series1 = this._getKpiTargetSeries(request.Series, request.PeriodeType, dateTimePeriodes, seriesType, request.RangeFilter, request.GraphicType, true);
                    var series2 = this._getKpiActualSeries(request.Series, request.PeriodeType, dateTimePeriodes, seriesType, request.RangeFilter, request.GraphicType, true);
                    seriesResponse = series1.Concat(series2).ToList();
                    seriesType = "multi-stacks-grouped";
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
                                periodes.Add(Start.Value.ToString(hourlyFormat));
                                dateTimePeriodes.Add(Start.Value);
                                Start = Start.Value.AddHours(1);
                            }
                            break;
                    }
                    break;
                case PeriodeType.Daily:
                    var dailyFormat = "MM/dd/yyyy";
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
                                periodes.Add(Start.Value.ToString(dailyFormat));
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
                            {
                                var currentMonth = DateTime.Now.Date;
                                dateTimePeriodes.Add(currentMonth);
                                periodes.Add(currentMonth.ToString(monthlyFormat));
                            }
                            break;
                        case RangeFilter.CurrentYear:
                            {
                                var currentYear = DateTime.Now.Year;
                                var startMonth = new DateTime(DateTime.Now.Year, 1, 1);

                                while (currentYear == startMonth.Year)
                                {
                                    periodes.Add(startMonth.ToString(monthlyFormat));
                                    dateTimePeriodes.Add(startMonth);
                                    startMonth = startMonth.AddMonths(1);
                                }
                            }
                            break;
                        case RangeFilter.YTD:
                            {
                                var currentYear = DateTime.Now.Year;
                                var startMonth = new DateTime(DateTime.Now.Year, 1, 1);
                                var currentMont = DateTime.Now.Month;
                                while (startMonth.Month <= currentMont)
                                {
                                    periodes.Add(startMonth.ToString(monthlyFormat));
                                    dateTimePeriodes.Add(startMonth);
                                    startMonth = startMonth.AddMonths(1);
                                }
                            }
                            break;
                        default:
                            while (Start.Value <= End.Value)
                            {
                                dateTimePeriodes.Add(Start.Value);
                                periodes.Add(Start.Value.ToString(monthlyFormat));
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
                                periodes.Add(Start.Value.ToString(yearlyFormat));
                                dateTimePeriodes.Add(Start.Value);
                                Start = Start.Value.AddYears(1);
                            }
                            break;
                    }
                    break;
            }

            return periodes.ToArray();
        }

        private IList<GetCartesianChartDataResponse.SeriesResponse> _getKpiTargetSeries(IList<GetCartesianChartDataRequest.SeriesRequest> configSeries, PeriodeType periodeType, IList<DateTime> dateTimePeriodes, string seriesType, RangeFilter rangeFilter, string graphicType, bool comparison = false)
        {
            var seriesResponse = new List<GetCartesianChartDataResponse.SeriesResponse>();
            var start = dateTimePeriodes[0];
            var end = dateTimePeriodes[dateTimePeriodes.Count - 1];
            foreach (var series in configSeries)
            {

                if (series.Stacks.Count == 0)
                {
                    var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == periodeType &&
                      x.Periode >= start && x.Periode <= end && x.Kpi.Id == series.KpiId)
                      .OrderBy(x => x.Periode).ToList();

                    if (seriesType == "multi-stacks-grouped")
                    {
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Stack = series.Label,
                            Color = series.Color
                        };
                        if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                        {

                            foreach (var periode in dateTimePeriodes)
                            {
                                var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                if (targetValue == null || !targetValue.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(targetValue.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(target.Value.Value);
                                }
                            }
                        }

                        seriesResponse.Add(aSeries);
                        if (graphicType == "baraccumulative")
                        {
                            var previousSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = "Previous",
                                Color = "#004071",
                                Stack = series.Label
                            };
                            for (var i = 0; i < aSeries.Data.Count; i++)
                            {
                                double data = 0;
                                for (var j = 0; j < i; j++)
                                {
                                    data += aSeries.Data[j];
                                }
                                previousSeries.Data.Add(data);
                            }
                            seriesResponse.Add(previousSeries);
                        }
                    }
                    else
                    {
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Color = series.Color
                        };
                        if(comparison){
                            aSeries.Stack = "KpiTarget";
                        }
                        if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                        {

                            foreach (var periode in dateTimePeriodes)
                            {
                                var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                if (targetValue == null || !targetValue.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(targetValue.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(target.Value.Value);
                                }
                            }
                        }
                        seriesResponse.Add(aSeries);
                        if (graphicType == "baraccumulative")
                        {
                            var previousSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = "Previous",
                                Color = "#004071"
                            };
                            if (comparison)
                            {
                                previousSeries.Stack = "KpiTarget";
                            }
                            for (var i = 0; i < aSeries.Data.Count; i++)
                            {
                                double data = 0;
                                for (var j = 0; j < i; j++)
                                {
                                    data += aSeries.Data[j];
                                }
                                previousSeries.Data.Add(data);
                            }
                            seriesResponse.Add(previousSeries);
                        }
                    }

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
                            var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = stack.Label,
                                Stack = series.Label,
                                Color = stack.Color
                            };
                            if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                            {

                                foreach (var periode in dateTimePeriodes)
                                {
                                    var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                        .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                    if (targetValue == null || !targetValue.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(targetValue.Value);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var periode in dateTimePeriodes)
                                {
                                    var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                    }
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                        else
                        {

                            var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = stack.Label,
                                Color = stack.Color
                            };
                            if (comparison)
                            {
                                aSeries.Stack = "KpiTarget";
                            }
                            if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                            {

                                foreach (var periode in dateTimePeriodes)
                                {
                                    var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                        .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                    if (targetValue == null || !targetValue.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(targetValue.Value);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var periode in dateTimePeriodes)
                                {
                                    var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                    }
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                    }
                }
            }
            return seriesResponse;
        }

        private IList<GetCartesianChartDataResponse.SeriesResponse> _getKpiActualSeries(IList<GetCartesianChartDataRequest.SeriesRequest> configSeries, PeriodeType periodeType, IList<DateTime> dateTimePeriodes, string seriesType, RangeFilter rangeFilter, string graphicType, bool comparison = false)
        {
            var seriesResponse = new List<GetCartesianChartDataResponse.SeriesResponse>();
            var start = dateTimePeriodes[0];
            var end = dateTimePeriodes[dateTimePeriodes.Count - 1];
            foreach (var series in configSeries)
            {

                if (series.Stacks.Count == 0)
                {
                    var kpiActuals = DataContext.KpiAchievements.Where(x => x.PeriodeType == periodeType &&
                      x.Periode >= start && x.Periode <= end && x.Kpi.Id == series.KpiId)
                      .OrderBy(x => x.Periode).ToList();
                    if (seriesType == "multi-stacks-grouped" && graphicType == "baraccumulative")
                    {
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Stack = series.Label,
                            Color = series.Color
                        };
                        if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                        {

                            foreach (var periode in dateTimePeriodes)
                            {
                                var targetValue = kpiActuals.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                if (targetValue == null || !targetValue.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(targetValue.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiActuals.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(target.Value.Value);
                                }
                            }
                        }
                        seriesResponse.Add(aSeries);

                        var previousSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = "Previous",
                            Color = "#004071",
                            Stack = series.Label
                        };
                        for (var i = 0; i < aSeries.Data.Count; i++)
                        {
                            double data = 0;
                            for (var j = 0; j < i; j++)
                            {
                                data += aSeries.Data[j];
                            }
                            previousSeries.Data.Add(data);
                        }
                        seriesResponse.Add(previousSeries);
                    }
                    else if (seriesType == "multi-stacks" && graphicType == "baraccumulative")
                    {
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Color = series.Color
                        };
                        if (comparison)
                        {
                            aSeries.Stack = "KpiActual";
                        }
                        if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                        {

                            foreach (var periode in dateTimePeriodes)
                            {
                                var targetValue = kpiActuals.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                if (targetValue == null || !targetValue.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(targetValue.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiActuals.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(target.Value.Value);
                                }
                            }
                        }
                        seriesResponse.Add(aSeries);
                        var previousSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = "Previous",
                            Color = "#004071"
                        };
                        if (comparison)
                        {
                            previousSeries.Stack = "KpiActual";
                        }
                        for (var i = 0; i < aSeries.Data.Count; i++)
                        {
                            double data = 0;
                            for (var j = 0; j < i; j++)
                            {
                                data += aSeries.Data[j];
                            }
                            previousSeries.Data.Add(data);
                        }
                        seriesResponse.Add(previousSeries);
                    }
                    else if (seriesType == "multi-stacks" && graphicType == "barachievement")
                    {
                        var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == periodeType &&
                            x.Periode >= start && x.Periode <= end && x.Kpi.Id == series.KpiId)
                            .OrderBy(x => x.Periode).ToList();
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Color = string.IsNullOrEmpty(series.Color) ? "blue" : series.Color
                        };
                        if (comparison)
                        {
                            aSeries.Stack = "KpiActual";
                        }
                        var remainSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = "Remain",
                            Color = "red"
                        };
                        if (comparison)
                        {
                            remainSeries.Stack = "KpiActual";
                        }
                        var exceedSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = "Exceed",
                            Color = "green"
                        };
                        if (comparison)
                        {
                            exceedSeries.Stack = "KpiActual";
                        }
                        foreach (var periode in dateTimePeriodes)
                        {
                            if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                            {
                                var actual = kpiActuals.Where(x => x.Periode <= periode)
                                    .GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                var target = kpiTargets.Where(x => x.Periode <= periode)
                                    .GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();

                                if (!actual.HasValue)
                                {
                                    if (!target.HasValue)
                                    {
                                        exceedSeries.Data.Add(0);
                                        remainSeries.Data.Add(0);
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(0);
                                        remainSeries.Data.Add(target.Value);
                                        exceedSeries.Data.Add(0);
                                    }
                                }
                                else
                                {
                                    if (!target.HasValue)
                                    {
                                        aSeries.Data.Add(target.Value);
                                        remainSeries.Data.Add(0);
                                        exceedSeries.Data.Add(actual.Value);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value);
                                        var remain = target.Value - actual.Value;
                                        if (remain > 0)
                                        {
                                            remainSeries.Data.Add(remain);
                                            exceedSeries.Data.Add(0);
                                        }
                                        else
                                        {
                                            exceedSeries.Data.Add(-remain);
                                            remainSeries.Data.Add(0);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                var actual = kpiActuals.Where(x => x.Periode == periode).FirstOrDefault();
                                var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                if (actual == null || !actual.Value.HasValue)
                                {
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        exceedSeries.Data.Add(0);
                                        remainSeries.Data.Add(0);
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(0);
                                        remainSeries.Data.Add(target.Value.Value);
                                        exceedSeries.Data.Add(0);
                                    }
                                }
                                else
                                {
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                        remainSeries.Data.Add(0);
                                        exceedSeries.Data.Add(actual.Value.Value);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                        var remain = target.Value.Value - actual.Value.Value;
                                        if (remain > 0)
                                        {
                                            remainSeries.Data.Add(remain);
                                            exceedSeries.Data.Add(0);
                                        }
                                        else
                                        {
                                            exceedSeries.Data.Add(-remain);
                                            remainSeries.Data.Add(0);
                                        }
                                    }
                                }
                            }


                        }
                        seriesResponse.Add(remainSeries);
                        seriesResponse.Add(exceedSeries);
                        seriesResponse.Add(aSeries);
                    }
                    else
                    {
                        var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == periodeType &&
                             x.Periode >= start && x.Periode <= end && x.Kpi.Id == series.KpiId)
                             .OrderBy(x => x.Periode).ToList();
                        var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                        {
                            Name = series.Label,
                            Color = series.Color
                        };
                        if (comparison)
                        {
                            aSeries.Stack = "KpiActual";
                        }
                        if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                        {

                            foreach (var periode in dateTimePeriodes)
                            {
                                var targetValue = kpiActuals.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                    .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                if (targetValue == null || !targetValue.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(targetValue.Value);
                                }
                            }
                        }
                        else
                        {
                            foreach (var periode in dateTimePeriodes)
                            {
                                var target = kpiActuals.Where(x => x.Periode == periode).FirstOrDefault();
                                if (target == null || !target.Value.HasValue)
                                {
                                    aSeries.Data.Add(0);
                                }
                                else
                                {
                                    aSeries.Data.Add(target.Value.Value);
                                }
                            }
                        }
                        seriesResponse.Add(aSeries);
                        if (graphicType == "baraccumulative")
                        {
                            var previousSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = "Previous",
                                Color = "#004071"
                            };
                            if (comparison)
                            {
                                previousSeries.Stack = "KpiActual";
                            }
                            for (var i = 0; i < aSeries.Data.Count; i++)
                            {
                                double data = 0;
                                for (var j = 0; j < i; j++)
                                {
                                    data += aSeries.Data[j];
                                }
                                previousSeries.Data.Add(data);
                            }
                            seriesResponse.Add(previousSeries);
                        }
                    }
                }
                else
                {
                    foreach (var stack in series.Stacks)
                    {
                        var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == periodeType &&
                        x.Periode >= start && x.Periode <= end && x.Kpi.Id == stack.KpiId)
                        .OrderBy(x => x.Periode).ToList();
                        if (seriesType == "multi-stacks-grouped")
                        {
                            var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = stack.Label,
                                Stack = series.Label,
                                Color = stack.Color
                            };
                            if (comparison)
                            {
                                aSeries.Stack = "KpiActual";
                            }
                            if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                            {

                                foreach (var periode in dateTimePeriodes)
                                {
                                    var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                        .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                    if (targetValue == null || !targetValue.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(targetValue.Value);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var periode in dateTimePeriodes)
                                {
                                    var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                    }
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                        else
                        {

                            var aSeries = new GetCartesianChartDataResponse.SeriesResponse
                            {
                                Name = stack.Label,
                                Color = stack.Color
                            };
                            if (comparison)
                            {
                                aSeries.Stack = "KpiActual";
                            }
                            if (rangeFilter == RangeFilter.YTD || rangeFilter == RangeFilter.DTD || rangeFilter == RangeFilter.MTD)
                            {

                                foreach (var periode in dateTimePeriodes)
                                {
                                    var targetValue = kpiTargets.Where(x => x.Periode <= periode).GroupBy(x => x.Kpi)
                                        .Select(x => x.Sum(y => y.Value)).FirstOrDefault();
                                    if (targetValue == null || !targetValue.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(targetValue.Value);
                                    }
                                }
                            }
                            else
                            {
                                foreach (var periode in dateTimePeriodes)
                                {
                                    var target = kpiTargets.Where(x => x.Periode == periode).FirstOrDefault();
                                    if (target == null || !target.Value.HasValue)
                                    {
                                        aSeries.Data.Add(0);
                                    }
                                    else
                                    {
                                        aSeries.Data.Add(target.Value.Value);
                                    }
                                }
                            }
                            seriesResponse.Add(aSeries);
                        }
                    }
                }
            }
            return seriesResponse;
        }


        public CreateArtifactResponse Create(CreateArtifactRequest request)
        {
            var artifact = request.MapTo<Artifact>();
            var measurement = new Measurement { Id = request.MeasurementId };
            DataContext.Measurements.Attach(measurement);
            artifact.Measurement = measurement;

            foreach (var seriesReq in request.Series)
            {
                var series = seriesReq.MapTo<ArtifactSerie>();
                if (seriesReq.KpiId != 0)
                {
                    var kpi = new Kpi { Id = seriesReq.KpiId };
                    if (DataContext.Kpis.Local.Where(x => x.Id == seriesReq.KpiId).FirstOrDefault() == null)
                    {
                        DataContext.Kpis.Attach(kpi);
                    }
                    else
                    {
                        kpi = DataContext.Kpis.Local.Where(x => x.Id == seriesReq.KpiId).FirstOrDefault();
                    }
                    series.Kpi = kpi;
                }
                foreach (var stackReq in seriesReq.Stacks)
                {
                    var stack = stackReq.MapTo<ArtifactStack>();
                    if (stackReq.KpiId != 0)
                    {
                        var kpiInStack = new Kpi { Id = stackReq.KpiId };
                        if (DataContext.Kpis.Local.Where(x => x.Id == stackReq.KpiId).FirstOrDefault() == null)
                        {
                            DataContext.Kpis.Attach(kpiInStack);
                        }
                        else
                        {
                            kpiInStack = DataContext.Kpis.Local.Where(x => x.Id == stackReq.KpiId).FirstOrDefault();
                        }
                        stack.Kpi = kpiInStack;
                    }
                    series.Stacks.Add(stack);
                }
                artifact.Series.Add(series);
            }
            foreach (var plotReq in request.Plots)
            {
                var plot = plotReq.MapTo<ArtifactPlot>();
                artifact.Plots.Add(plot);
            }
            foreach (var rowReq in request.Rows)
            {
                var row  = rowReq.MapTo<ArtifactRow>();
                if (rowReq.KpiId != 0)
                {
                    var kpiInRow = new Kpi { Id = rowReq.KpiId };
                    if (DataContext.Kpis.Local.Where(x => x.Id == rowReq.KpiId).FirstOrDefault() == null)
                    {
                        DataContext.Kpis.Attach(kpiInRow);
                    }
                    else
                    {
                        kpiInRow = DataContext.Kpis.Local.Where(x => x.Id == rowReq.KpiId).FirstOrDefault();
                    }
                    row.Kpi = kpiInRow;
                }
                artifact.Rows.Add(row);
            }
            DataContext.Artifacts.Add(artifact);
            DataContext.SaveChanges();
            return new CreateArtifactResponse();
        }

        public UpdateArtifactResponse Update(UpdateArtifactRequest request)
        {
            var artifact = DataContext.Artifacts.Include(x => x.Measurement)
                .Include(x => x.Series)
                .Include(x => x.Series.Select(y => y.Kpi))
                .Include(x => x.Series.Select(y => y.Stacks))
                .Include(x => x.Series.Select(y => y.Stacks.Select(z => z.Kpi)))
                .Include(x => x.Plots)
                .FirstOrDefault(x => x.Id == request.Id);

            if (artifact.Measurement.Id != request.MeasurementId) {
                var measurement = new Measurement { Id = request.MeasurementId };
                DataContext.Measurements.Attach(measurement);
                artifact.Measurement = measurement;
            
            }
            foreach (var series in artifact.Series.ToList()) {
                foreach (var stack in series.Stacks.ToList()) {
                    DataContext.ArtifactStacks.Remove(stack);
                }
                DataContext.ArtifactSeries.Remove(series);
            }
            foreach (var plot in artifact.Plots.ToList()) {
                DataContext.ArtifactPlots.Remove(plot);
            }

            foreach (var seriesReq in request.Series)
            {
                var series = seriesReq.MapTo<ArtifactSerie>();
                if (seriesReq.KpiId != 0)
                {
                    var kpi = new Kpi { Id = seriesReq.KpiId };
                    if (DataContext.Kpis.Local.Where(x => x.Id == seriesReq.KpiId).FirstOrDefault() == null)
                    {
                        DataContext.Kpis.Attach(kpi);
                    }
                    else
                    {
                        kpi = DataContext.Kpis.Local.Where(x => x.Id == seriesReq.KpiId).FirstOrDefault();
                    }
                    series.Kpi = kpi;
                }
                foreach (var stackReq in seriesReq.Stacks)
                {
                    var stack = stackReq.MapTo<ArtifactStack>();
                    if (stackReq.KpiId != 0)
                    {
                        var kpiInStack = new Kpi { Id = stackReq.KpiId };
                        if (DataContext.Kpis.Local.Where(x => x.Id == stackReq.KpiId).FirstOrDefault() == null)
                        {
                            DataContext.Kpis.Attach(kpiInStack);
                        }
                        else
                        {
                            kpiInStack = DataContext.Kpis.Local.Where(x => x.Id == stackReq.KpiId).FirstOrDefault();
                        }
                        stack.Kpi = kpiInStack;
                    }
                    series.Stacks.Add(stack);
                }
                artifact.Series.Add(series);
            }
            foreach (var plotReq in request.Plots)
            {
                var plot = plotReq.MapTo<ArtifactPlot>();
                artifact.Plots.Add(plot);
            }
            DataContext.SaveChanges();
            return new UpdateArtifactResponse();
        }


        public GetArtifactsResponse GetArtifacts(GetArtifactsRequest request)
        {
            if (request.OnlyCount)
            {
                return new GetArtifactsResponse { Count = DataContext.Artifacts.Count() };
            }
            else
            {
                return new GetArtifactsResponse
                {
                    Artifacts = DataContext.Artifacts.OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take)
                                    .ToList().MapTo<GetArtifactsResponse.Artifact>()
                };
            }
        }

        public GetArtifactResponse GetArtifact(GetArtifactRequest request)
        {
            return DataContext.Artifacts.Include(x => x.Measurement)
                .Include(x => x.Series)
                .Include(x => x.Series.Select(y => y.Kpi))
                .Include(x => x.Series.Select(y => y.Stacks))
                .Include(x => x.Series.Select(y => y.Stacks.Select(z => z.Kpi)))
                .Include(x => x.Plots)
                .FirstOrDefault(x => x.Id == request.Id).MapTo<GetArtifactResponse>();
        }

        public GetArtifactsToSelectResponse GetArtifactsToSelect(GetArtifactsToSelectRequest request)
        {
            return new GetArtifactsToSelectResponse
            {
                Artifacts = DataContext.Artifacts.Where(x => x.GraphicName.Contains(request.Term)).Take(20).ToList()
                .MapTo<GetArtifactsToSelectResponse.ArtifactResponse>()
            };
        }
    }
}
