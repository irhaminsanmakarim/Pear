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
using DevExpress.Web.Mvc;

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

        public ActionResult Index() {
            return View();
        }
        public ActionResult IndexPartial()
        {
            var viewModel = GridViewExtension.GetViewModel("gridArtifactIndex");
            if (viewModel == null)
                viewModel = CreateGridViewModel();
            return BindingCore(viewModel);
        }

        PartialViewResult BindingCore(GridViewModel gridViewModel)
        {
            gridViewModel.ProcessCustomBinding(
                GetDataRowCount,
                GetData
            );
            return PartialView("_IndexGridPartial", gridViewModel);
        }

        static GridViewModel CreateGridViewModel()
        {
            var viewModel = new GridViewModel();
            viewModel.KeyFieldName = "Id";
            viewModel.Columns.Add("GraphicName");
            viewModel.Columns.Add("GraphicType");
            viewModel.Pager.PageSize = 10;
            return viewModel;
        }

        public ActionResult PagingAction(GridViewPagerState pager)
        {
            var viewModel = GridViewExtension.GetViewModel("gridArtifactIndex");
            viewModel.ApplyPagingState(pager);
            return BindingCore(viewModel);
        }

        public void GetDataRowCount(GridViewCustomBindingGetDataRowCountArgs e)
        {

            e.DataRowCount = _artifactServie.GetArtifacts(new GetArtifactsRequest { OnlyCount = true }).Count;
        }

        public void GetData(GridViewCustomBindingGetDataArgs e)
        {
            e.Data = _artifactServie.GetArtifacts(new GetArtifactsRequest
            {
                Skip = e.StartDataRowIndex,
                Take = e.DataRowCount
            }).Artifacts;
        }

        public ActionResult Designer()
        {
            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "line", Text = "Line" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "speedometer", Text = "Speedometer" });

            viewModel.Measurements = _measurementService.GetMeasurements(new GetMeasurementsRequest()).Measurements
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            this.SetPeriodeTypes(viewModel.PeriodeTypes);
            this.SetRangeFilters(viewModel.RangeFilters);
            this.SetValueAxes(viewModel.ValueAxes);
            this.SetKpiList(viewModel.KpiList);
            return View(viewModel);
        }

        public ActionResult GraphSettings()
        {
            switch (Request.QueryString["type"])
            {
                case "bar":
                    {
                        var viewModel = new BarChartViewModel();
                       
                        viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.MultiStacks.ToString(), Text = "Multi Stacks" });
                        
                        var series = new BarChartViewModel.Series();
                        series.Stacks.Add(new BarChartViewModel.Stack());
                        viewModel.SeriesList.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.BarChart = viewModel;
                        return PartialView("~/Views/BarChart/_Create.cshtml", artifactViewModel);
                    }
                case "line":
                    {
                        var viewModel = new LineChartViewModel();
                        this.SetPeriodeTypes(viewModel.PeriodeTypes);
                        this.SetRangeFilters(viewModel.RangeFilters);
                        this.SetValueAxes(viewModel.ValueAxes);
                        this.SetKpiList(viewModel.KpiList);
                        var series = new LineChartViewModel.Series();
                        viewModel.SeriesList.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.LineChart = viewModel;
                        return PartialView("~/Views/LineChart/_Create.cshtml", artifactViewModel);
                    }
                case "speedometer":
                    {
                        var viewModel = new SpeedometerChartViewModel();
                        this.SetPeriodeTypes(viewModel.PeriodeTypes);
                        this.SetRangeFilters(viewModel.RangeFilters);
                        this.SetValueAxes(viewModel.ValueAxes);
                        this.SetKpiList(viewModel.KpiList);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.SpeedometerChart = viewModel;
                        return PartialView("~/Views/SpeedometerChart/_Create.cshtml", artifactViewModel);
                    }
                default:
                    return PartialView("NotImplementedChart.cshtml");
            }
        }

        public void SetKpiList(IList<SelectListItem> kpiList) {
            foreach(var kpi in _kpiService.GetKpiToSeries().KpiList){
                kpiList.Add(new SelectListItem { Value = kpi.Id.ToString(), Text = kpi.Name });
            }
        }

        public void SetValueAxes(IList<SelectListItem> valueAxes) {
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiTarget.ToString(), Text = "Kpi Target" });
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiActual.ToString(), Text = "Kpi Actual" });
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiEconomic.ToString(), Text = "Kpi Economic" });
            valueAxes.Add(new SelectListItem { Value = ValueAxis.Custom.ToString(), Text = "Uniqe Each Series" });
        }

        public void SetPeriodeTypes(IList<SelectListItem> periodeTypes) {
            foreach (var name in Enum.GetNames(typeof(PeriodeType))) {
                periodeTypes.Add(new SelectListItem { Text = name, Value = name });
            }
        }

        public void SetRangeFilters(IList<SelectListItem> rangeFilters) {
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentHour.ToString(), Text = "CURRENT HOUR" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentDay.ToString(), Text = "CURRENT DAY" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentWeek.ToString(), Text = "CURRENT WEEK" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentMonth.ToString(), Text = "CURRENT MONTH" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.CurrentYear.ToString(), Text = "CURRENT YEAR" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.DTD.ToString(), Text = "DAY TO DATE" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.MTD.ToString(), Text = "MONTH TO DATE" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.YTD.ToString(), Text = "YEAR TO DATE" });
            rangeFilters.Add(new SelectListItem { Value = RangeFilter.Interval.ToString(), Text = "INTERVAL" });
        }

        public ActionResult View(int id) {
            var artifactResp = _artifactServie.GetArtifact(new GetArtifactRequest { Id = id });
            var previewViewModel = new ArtifactPreviewViewModel();
            switch (artifactResp.GraphicType)
            {
                case "line":
                    {
                        var chartData = _artifactServie.GetChartData(artifactResp.MapTo<GetChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.LineChart = new LineChartDataViewModel();
                        previewViewModel.LineChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.LineChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.LineChart.Series = chartData.Series.MapTo<LineChartDataViewModel.SeriesViewModel>();
                        previewViewModel.LineChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "speedometer":
                    {
                        var chartData = _artifactServie.GetSpeedometerChartData(artifactResp.MapTo<GetSpeedometerChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.SpeedometerChart = new SpeedometerChartDataViewModel();
                        previewViewModel.SpeedometerChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.SpeedometerChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                default:
                    {
                        var chartData = _artifactServie.GetChartData(artifactResp.MapTo<GetChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.BarChart = new BarChartDataViewModel();
                        previewViewModel.BarChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.BarChart.ValueAxisTitle = artifactResp.Measurement; //.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.BarChart.Series = chartData.Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                        previewViewModel.BarChart.Periodes = chartData.Periodes;
                        previewViewModel.BarChart.SeriesType = chartData.SeriesType;
                    }
                    break;
            }
            return Json(previewViewModel,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Preview(ArtifactDesignerViewModel viewModel) {
            var previewViewModel = new ArtifactPreviewViewModel();
            switch (viewModel.GraphicType) {
                case "line":
                    {
                        var chartData = _artifactServie.GetChartData(viewModel.LineChart.MapTo<GetChartDataRequest>());
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.LineChart = new LineChartDataViewModel();
                        previewViewModel.LineChart.Title = viewModel.HeaderTitle;
                        previewViewModel.LineChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.LineChart.Series = chartData.Series.MapTo<LineChartDataViewModel.SeriesViewModel>();
                        previewViewModel.LineChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "speedometer":
                    {
                        var chartData = _artifactServie.GetSpeedometerChartData(viewModel.SpeedometerChart.MapTo<GetSpeedometerChartDataRequest>());
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.SpeedometerChart = new SpeedometerChartDataViewModel();
                        previewViewModel.SpeedometerChart.Title = viewModel.HeaderTitle;
                        previewViewModel.SpeedometerChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                default:
                    {
                        var chartData = _artifactServie.GetChartData(viewModel.BarChart.MapTo<GetChartDataRequest>());
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.BarChart = new BarChartDataViewModel();
                        previewViewModel.BarChart.Title = viewModel.HeaderTitle;
                        previewViewModel.BarChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.BarChart.Series = chartData.Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                        previewViewModel.BarChart.Periodes = chartData.Periodes;
                        previewViewModel.BarChart.SeriesType = chartData.SeriesType;
                    }
                    break;
            }
            return Json(previewViewModel);
        }

        [HttpPost]
        public ActionResult Create(ArtifactDesignerViewModel viewModel) {
            switch (viewModel.GraphicType) {
                case "line":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.LineChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;
                case "speedometer":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.SpeedometerChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;
                default:
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.BarChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;
            }
           return  RedirectToAction("Index");
        }
    }
}
