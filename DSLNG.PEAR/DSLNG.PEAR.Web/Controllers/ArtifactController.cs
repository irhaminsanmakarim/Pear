using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Web.ViewModels.Artifact;
using System;
using System.Linq;
using System.Web.Mvc;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Requests.Artifact;
using System.Collections.Generic;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ArtifactController : BaseController
    {
        public IMeasurementService _measurementService;
        public IKpiService _kpiService;
        public IArtifactService _artifactServie;

        public ArtifactController(IMeasurementService measurementService,
            IKpiService kpiService,
            IArtifactService artifactServcie) {
            _measurementService = measurementService;
            _kpiService = kpiService;
            _artifactServie = artifactServcie;
        }

        public ActionResult Designer()
        {
            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });

            viewModel.Measurements = _measurementService.GetMeasurements(new GetMeasurementsRequest()).Measurements
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            return View(viewModel);
        }

        public ActionResult GraphSettings()
        {
            switch (Request.QueryString["type"])
            {
                case "bar":
                    var viewModel = new BarChartViewModel();
                    viewModel.PeriodeTypes = Enum.GetNames(typeof(PeriodeType)).Select(x => new SelectListItem { Text = x, Value = x }).ToList();

                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentHour.ToString(), Text = "CURRENT HOUR" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentDay.ToString(), Text = "CURRENT DAY" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentWeek.ToString(), Text = "CURRENT WEEK" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentMonth.ToString(), Text = "CURRENT MONTH" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentYear.ToString(), Text = "CURRENT YEAR" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.DTD.ToString(), Text = "DAY TO DATE" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.MTD.ToString(), Text = "MONTH TO DATE" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.YTD.ToString(), Text = "YEAR TO DATE" });
                    viewModel.RangeFilters.Add(new SelectListItem { Value = RangeFilter.Interval.ToString(), Text = "INTERVAL" });

                    viewModel.ValueAxes.Add(new SelectListItem { Value = ValueAxis.KpiTarget.ToString(), Text = "Kpi Target" });
                    viewModel.ValueAxes.Add(new SelectListItem { Value = ValueAxis.KpiActual.ToString(), Text = "Kpi Actual" });
                    viewModel.ValueAxes.Add(new SelectListItem { Value = ValueAxis.KpiEconomic.ToString(), Text = "Kpi Economic" });
                    viewModel.ValueAxes.Add(new SelectListItem { Value = ValueAxis.Custom.ToString(), Text = "Uniqe Each Series" });
                    
                    viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                    viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.MultiStacks.ToString(), Text = "Multi Stacks" });
                     
                    viewModel.KpiList = _kpiService.GetKpiToSeries().KpiList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                    var series = new BarChartViewModel.Series();
                    series.Stacks.Add(new BarChartViewModel.Stack());
                    viewModel.SeriesList.Add(series);
                    var artifactViewModel = new ArtifactDesignerViewModel();
                    artifactViewModel.BarChart = viewModel;
                    return PartialView("~/Views/BarChart/_Create.cshtml", artifactViewModel);
                default:
                    return PartialView("NotImplementedChart.cshtml");
            }
        }
        [HttpPost]
        public ActionResult Preview(ArtifactDesignerViewModel viewModel) {
            var previewViewModel = new ArtifactPreviewViewModel();
            switch (viewModel.GraphicType) { 
                default:
                    var chartData = _artifactServie.GetChartData(viewModel.BarChart.MapTo<GetChartDataRequest>());
                    previewViewModel.GraphicType = viewModel.GraphicType;
                    previewViewModel.BarChart = new BarChartDataViewModel();
                    previewViewModel.BarChart.Title = viewModel.HeaderTitle;
                    previewViewModel.BarChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                    previewViewModel.BarChart.Series = chartData.Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                    previewViewModel.BarChart.Periodes = chartData.Periodes;
                    previewViewModel.BarChart.SeriesType = chartData.SeriesType;
                    //var periodes = new List<string>();
                    //if (viewModel.BarChart.PeriodeType == PeriodeType.Monthly.ToString() && viewModel.BarChart.RangeFilter == RangeFilter.CurrentYear.ToString()) { 
                    //    previewViewModel.BarChart.Periodes = new string[12] {"Jan", "Feb", "Mar", "Apr", "Mey", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec"};
                    //}
                    //previewViewModel.BarChart.Series = _artifactServie.GetSeries(viewModel.BarChart.MapTo<GetSeriesRequest>()).Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                    break;
            }
            return Json(previewViewModel);
            //return View(BarChartDataViewModel.GetSeries());
        }
    }
}
