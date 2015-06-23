using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Web.ViewModels.Artifact;
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
                    viewModel.PeriodeTypes.Add(new SelectListItem { Value = "monthly", Text = "Monthly" });
                    viewModel.PeriodeTypes.Add(new SelectListItem { Value = "yearly", Text = "Yearly" });
                    viewModel.PeriodeTypes.Add(new SelectListItem { Value = "ytd", Text = "Year To Date" });
                    viewModel.ValueAxises.Add(new SelectListItem { Value = "kpi-target", Text = "Kpi Target" });
                    viewModel.ValueAxises.Add(new SelectListItem { Value = "kpi-actual", Text = "Kpi Actual" });
                    viewModel.ValueAxises.Add(new SelectListItem { Value = "kpi-economic", Text = "Kpi Economic" });
                    viewModel.ValueAxises.Add(new SelectListItem { Value = "unique-axis", Text = "Uniqe Each Series" });
                    viewModel.SeriesTypes.Add(new SelectListItem { Value = "single", Text = "Single Stack" });
                    viewModel.SeriesTypes.Add(new SelectListItem { Value = "multiple", Text = "Multiple Stack" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "no-aggregation", Text = "-" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "sum", Text = "SUM" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "min", Text = "SUM" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "max", Text = "SUM" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "avg", Text = "SUM" });
                    viewModel.Aggragations.Add(new SelectListItem { Value = "count", Text = "SUM" });
                    
                    viewModel.KpiList = _kpiService.GetKpiToSeries().KpiList.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
                    var series = new BarChartViewModel.Series();
                    series.Stacks.Add(new BarChartViewModel.Stack());
                    viewModel.SeriesList.Add(series);

                    return PartialView("~/Views/BarChart/_Create.cshtml", viewModel);
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
