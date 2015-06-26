using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Web.ViewModels.Artifact;
using System;
using System.Linq;
using System.Web.Mvc;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ArtifactController : BaseController
    {
        public IMeasurementService _measurementService;
        public IKpiService _kpiService;
        public ArtifactController(IMeasurementService measurementService,
            IKpiService kpiService) {
            _measurementService = measurementService;
            _kpiService = kpiService;
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

            return View(BarChartDataViewModel.GetSeries());
        }
    }
}
