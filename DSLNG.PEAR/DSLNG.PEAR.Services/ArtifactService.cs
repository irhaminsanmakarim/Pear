

using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;
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


        public GetSeriesResponse GetSeries(GetSeriesRequest request)
        {
            var seriesResponse = new List<GetSeriesResponse.SeriesResponse>();
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
                    {
                        foreach (var series in request.SeriesList)
                        {

                            if (series.Stacks.Count == 0)
                            {
                                var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                  x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == series.KpiId)
                                  .OrderBy(x => x.Periode);
                                var aSeries = new GetSeriesResponse.SeriesResponse
                                {
                                    name = series.Label
                                };
                                foreach (var target in kpiTargets)
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                                seriesResponse.Add(aSeries);
                            }
                            else
                            {
                                foreach (var stack in series.Stacks)
                                {
                                    var kpiTargets = DataContext.KpiTargets.Where(x => x.PeriodeType == request.PeriodeType &&
                                  x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == stack.KpiId)
                                  .OrderBy(x => x.Periode);
                                    if (seriesType == "multi-stacks-grouped")
                                    {
                                        var aSeries = new GetSeriesResponse.SeriesResponse
                                        {
                                            name = stack.Label,
                                            stack = series.Label
                                        };
                                        foreach (var target in kpiTargets)
                                        {
                                            aSeries.data.Add(target.Value.Value);
                                        }
                                        seriesResponse.Add(aSeries);
                                    }
                                    else
                                    {
                                        var aSeries = new GetSeriesResponse.SeriesResponse
                                        {
                                            name = series.Label
                                        };
                                        foreach (var target in kpiTargets)
                                        {
                                            aSeries.data.Add(target.Value.Value);
                                        }
                                        seriesResponse.Add(aSeries);
                                    }
                                }
                            }
                        }

                    }
                    break;
                case ValueAxis.KpiActual:
                    {
                        foreach (var series in request.SeriesList)
                        {

                            if (series.Stacks.Count == 0)
                            {
                                var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                  x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == series.KpiId)
                                  .OrderBy(x => x.Periode);
                                var aSeries = new GetSeriesResponse.SeriesResponse
                                {
                                    name = series.Label
                                };
                                foreach (var target in kpiTargets)
                                {
                                    aSeries.data.Add(target.Value.Value);
                                }
                                seriesResponse.Add(aSeries);
                            }
                            else
                            {
                                foreach (var stack in series.Stacks)
                                {
                                    var kpiTargets = DataContext.KpiAchievements.Where(x => x.PeriodeType == request.PeriodeType &&
                                  x.Periode >= request.Start && x.Periode <= request.End && x.Kpi.Id == stack.KpiId)
                                  .OrderBy(x => x.Periode);
                                    if (seriesType == "multi-stacks-grouped")
                                    {
                                        var aSeries = new GetSeriesResponse.SeriesResponse
                                        {
                                            name = stack.Label,
                                            stack = series.Label
                                        };
                                        foreach (var target in kpiTargets)
                                        {
                                            aSeries.data.Add(target.Value.Value);
                                        }
                                        seriesResponse.Add(aSeries);
                                    }
                                    else
                                    {
                                        var aSeries = new GetSeriesResponse.SeriesResponse
                                        {
                                            name = series.Label
                                        };
                                        foreach (var target in kpiTargets)
                                        {
                                            aSeries.data.Add(target.Value.Value);
                                        }
                                        seriesResponse.Add(aSeries);
                                    }
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return new GetSeriesResponse
            {
                Series = seriesResponse
            };

        }
    }
}
