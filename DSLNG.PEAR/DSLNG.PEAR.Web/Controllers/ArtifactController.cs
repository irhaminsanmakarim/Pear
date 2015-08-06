using System.Globalization;
using System.Threading;
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
using DSLNG.PEAR.Services.Requests.Kpi;
using PeriodeType = DSLNG.PEAR.Data.Enums.PeriodeType;

namespace DSLNG.PEAR.Web.Controllers
{
    public class ArtifactController : BaseController
    {
        private readonly IMeasurementService _measurementService;
        private readonly IKpiService _kpiService;
        private readonly IArtifactService _artifactServie;

        public ArtifactController(IMeasurementService measurementService,
            IKpiService kpiService,
            IArtifactService artifactServcie)
        {
            _measurementService = measurementService;
            _kpiService = kpiService;
            _artifactServie = artifactServcie;
        }

        public ActionResult Index()
        {
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


        public ActionResult KpiList(SearchKpiViewModel viewModel)
        {
            var kpis = _kpiService.GetKpiToSeries(viewModel.MapTo<GetKpiToSeriesRequest>()).KpiList;
            return Json(new { results = kpis }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Designer()
        {
            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "baraccumulative", Text = "Bar Accumulative" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "barachievement", Text = "Bar Achievement" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "line", Text = "Line" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "area", Text = "Area" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "multiaxis", Text = "Multi Axis" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "speedometer", Text = "Speedometer" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "trafficlight", Text = "Traffic Light" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "tabular", Text = "Tabular" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "tank", Text = "Tank" });
            viewModel.Measurements = _measurementService.GetMeasurements(new GetMeasurementsRequest()).Measurements
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            this.SetPeriodeTypes(viewModel.PeriodeTypes);
            this.SetRangeFilters(viewModel.RangeFilters);
            this.SetValueAxes(viewModel.ValueAxes);
            //this.SetKpiList(viewModel.KpiList);
            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            var artifact = _artifactServie.GetArtifact(new GetArtifactRequest { Id = id });

            var viewModel = new ArtifactDesignerViewModel();
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "baraccumulative", Text = "Bar Accumulative" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "barachievement", Text = "Bar Achievement" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "line", Text = "Line" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "area", Text = "Area" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "multiaxis", Text = "Multi Axis" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "speedometer", Text = "Speedometer" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "tabular", Text = "Tabular" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "tank", Text = "Tank" });
            viewModel.GraphicTypes.Add(new SelectListItem { Value = "trafficlight", Text = "Traffic Light" });
            

            viewModel.Measurements = _measurementService.GetMeasurements(new GetMeasurementsRequest()).Measurements
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            this.SetPeriodeTypes(viewModel.PeriodeTypes);
            this.SetRangeFilters(viewModel.RangeFilters);
            this.SetValueAxes(viewModel.ValueAxes);
            artifact.MapPropertiesToInstance<ArtifactDesignerViewModel>(viewModel);
            switch (viewModel.GraphicType)
            {
                case "speedometer":
                    {
                        var speedometerChart = new SpeedometerChartViewModel();
                        viewModel.SpeedometerChart = artifact.MapPropertiesToInstance<SpeedometerChartViewModel>(speedometerChart);
                        var plot = new SpeedometerChartViewModel.PlotBand();
                        viewModel.SpeedometerChart.PlotBands.Insert(0, plot);
                    }
                    break;
                case "line":
                    {
                        var lineChart = new LineChartViewModel();
                        viewModel.LineChart = artifact.MapPropertiesToInstance<LineChartViewModel>(lineChart);
                        this.SetValueAxes(viewModel.LineChart.ValueAxes);
                        var series = new LineChartViewModel.SeriesViewModel();
                        viewModel.LineChart.Series.Insert(0, series);
                    }
                    break;
                case "area":
                    {
                        var areaChart = new AreaChartViewModel();
                        viewModel.AreaChart = artifact.MapPropertiesToInstance<AreaChartViewModel>(areaChart);
                        this.SetValueAxes(viewModel.AreaChart.ValueAxes);
                        var series = new AreaChartViewModel.SeriesViewModel();
                        viewModel.AreaChart.Series.Insert(0, series);
                    }
                    break;
                case "barachievement":
                    {
                        var barChart = new BarChartViewModel();
                        barChart.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        this.SetValueAxes(barChart.ValueAxes, false);
                        viewModel.BarChart = artifact.MapPropertiesToInstance<BarChartViewModel>(barChart);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.BarChart.Series.Insert(0, series);
                    }
                    break;
                case "baraccumulative":
                    {
                        var barChart = new BarChartViewModel();
                        barChart.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        this.SetValueAxes(barChart.ValueAxes, false);
                        viewModel.BarChart = artifact.MapPropertiesToInstance<BarChartViewModel>(barChart);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.BarChart.Series.Insert(0, series);
                    }
                    break;
                case "trafficlight":
                    {
                        var trafficLightChart = new TrafficLightChartViewModel();
                        viewModel.TrafficLightChart = artifact.MapPropertiesToInstance<TrafficLightChartViewModel>(trafficLightChart);
                        var plot = new TrafficLightChartViewModel.PlotBand();
                        viewModel.TrafficLightChart.PlotBands.Insert(0, plot);
                    }
                    break;
                case "tank":
                    {
                        viewModel.Tank = artifact.Tank.MapTo<TankViewModel>();
                    }
                    break;
                case "tabular":
                    {
                        viewModel.Tabular = artifact.MapPropertiesToInstance(new TabularViewModel());
                        viewModel.Tabular.Rows.Insert(0, new TabularViewModel.RowViewModel());
                        this.SetPeriodeTypes(viewModel.Tabular.PeriodeTypes);
                        this.SetRangeFilters(viewModel.Tabular.RangeFilters);
                    }
                    break;
                default:
                    {
                        var barChart = new BarChartViewModel();
                        barChart.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        barChart.SeriesTypes.Add(new SelectListItem { Value = SeriesType.MultiStacks.ToString(), Text = "Multi Stacks" });
                        this.SetValueAxes(barChart.ValueAxes, false);

                        viewModel.BarChart = artifact.MapPropertiesToInstance<BarChartViewModel>(barChart);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.BarChart.Series.Insert(0, series);
                    }
                    break;
            }

            viewModel.StartInDisplay = ParseDateToString(artifact.PeriodeType, artifact.Start);
            viewModel.EndInDisplay = ParseDateToString(artifact.PeriodeType, artifact.End);
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
                        this.SetValueAxes(viewModel.ValueAxes, false);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.Series.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.BarChart = viewModel;
                        return PartialView("~/Views/BarChart/_Create.cshtml", artifactViewModel);
                    }
                case "baraccumulative":
                    {
                        var viewModel = new BarChartViewModel();
                        viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        this.SetValueAxes(viewModel.ValueAxes, false);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.Series.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.BarChart = viewModel;
                        return PartialView("~/Views/BarChart/_Create.cshtml", artifactViewModel);
                    }
                case "barachievement":
                    {
                        var viewModel = new BarChartViewModel();
                        viewModel.SeriesTypes.Add(new SelectListItem { Value = SeriesType.SingleStack.ToString(), Text = "Single Stack" });
                        this.SetValueAxes(viewModel.ValueAxes, false);
                        var series = new BarChartViewModel.SeriesViewModel();
                        series.Stacks.Add(new BarChartViewModel.StackViewModel());
                        viewModel.Series.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.BarChart = viewModel;
                        return PartialView("~/Views/BarChart/_Create.cshtml", artifactViewModel);
                    }
                case "line":
                    {
                        var viewModel = new LineChartViewModel();
                        this.SetValueAxes(viewModel.ValueAxes, false);
                        var series = new LineChartViewModel.SeriesViewModel();
                        viewModel.Series.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.LineChart = viewModel;
                        return PartialView("~/Views/LineChart/_Create.cshtml", artifactViewModel);
                    }
                case "area":
                    {
                        var viewModel = new AreaChartViewModel();
                        this.SetValueAxes(viewModel.ValueAxes, false);
                        var series = new AreaChartViewModel.SeriesViewModel();
                        viewModel.Series.Add(series);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.AreaChart = viewModel;
                        return PartialView("~/Views/AreaChart/_Create.cshtml", artifactViewModel);
                    }
                case "multiaxis":
                    {
                        var viewModel = new MultiaxisChartViewModel();
                        var chart = new MultiaxisChartViewModel.ChartViewModel();
                        this.SetValueAxes(viewModel.ValueAxes);
                        viewModel.GraphicTypes.Add(new SelectListItem { Value = "bar", Text = "Bar" });
                        viewModel.GraphicTypes.Add(new SelectListItem { Value = "baraccumulative", Text = "Bar Accumulative" });
                        viewModel.GraphicTypes.Add(new SelectListItem { Value = "barachievement", Text = "Bar Achievement" });
                        viewModel.GraphicTypes.Add(new SelectListItem { Value = "line", Text = "Line" });
                        viewModel.GraphicTypes.Add(new SelectListItem { Value = "area", Text = "Area" });
                        viewModel.Charts.Add(chart);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.MultiaxisChart = viewModel;
                        return PartialView("~/Views/MultiaxisChart/_Create.cshtml", artifactViewModel);
                    }
                case "speedometer":
                    {
                        var viewModel = new SpeedometerChartViewModel();
                        var plot = new SpeedometerChartViewModel.PlotBand();
                        viewModel.PlotBands.Add(plot);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.SpeedometerChart = viewModel;
                        return PartialView("~/Views/SpeedometerChart/_Create.cshtml", artifactViewModel);
                    }
                case "trafficlight":
                    {
                        var viewModel = new TrafficLightChartViewModel();
                        var plot = new TrafficLightChartViewModel.PlotBand();
                        viewModel.PlotBands.Add(plot);
                        var trafficLightViewModel = new ArtifactDesignerViewModel();
                        trafficLightViewModel.TrafficLightChart = viewModel;
                        return PartialView("~/Views/TrafficLightChart/_Create.cshtml", trafficLightViewModel);
                    }

                case "tabular":
                    {
                        var viewModel = new TabularViewModel();
                        var row = new TabularViewModel.RowViewModel();
                        this.SetPeriodeTypes(viewModel.PeriodeTypes);
                        this.SetRangeFilters(viewModel.RangeFilters);
                        viewModel.Rows.Add(row);
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.Tabular = viewModel;
                        return PartialView("~/Views/Tabular/_Create.cshtml", artifactViewModel);
                    }
                case "tank":
                    {
                        var viewModel = new TankViewModel();
                        var artifactViewModel = new ArtifactDesignerViewModel();
                        artifactViewModel.Tank = viewModel;
                        return PartialView("~/Views/Tank/_Create.cshtml", artifactViewModel);
                    }
                default:
                    return PartialView("NotImplementedChart.cshtml");
            }
        }

        public void SetValueAxes(IList<SelectListItem> valueAxes, bool isCustomIncluded = true)
        {
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiTarget.ToString(), Text = "Kpi Target" });
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiActual.ToString(), Text = "Kpi Actual" });
            valueAxes.Add(new SelectListItem { Value = ValueAxis.KpiEconomic.ToString(), Text = "Kpi Economic" });
            if (isCustomIncluded)
            {
                valueAxes.Add(new SelectListItem { Value = ValueAxis.Custom.ToString(), Text = "Uniqe Each Series" });
            }
        }

        public void SetPeriodeTypes(IList<SelectListItem> periodeTypes)
        {
            foreach (var name in Enum.GetNames(typeof(PeriodeType)))
            {
                periodeTypes.Add(new SelectListItem { Text = name, Value = name });
            }
        }

        public void SetRangeFilters(IList<SelectListItem> rangeFilters)
        {
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

        public ActionResult View(int id)
        {
            var artifactResp = _artifactServie.GetArtifact(new GetArtifactRequest { Id = id });
            var previewViewModel = new ArtifactPreviewViewModel();
            switch (artifactResp.GraphicType)
            {
                case "line":
                    {
                        var chartData = _artifactServie.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.LineChart = new LineChartDataViewModel();
                        previewViewModel.LineChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.LineChart.Subtitle = chartData.Subtitle;
                        previewViewModel.LineChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.LineChart.Series = chartData.Series.MapTo<LineChartDataViewModel.SeriesViewModel>();
                        previewViewModel.LineChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "area":
                    {
                        var chartData = _artifactServie.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.AreaChart = new AreaChartDataViewModel();
                        previewViewModel.AreaChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.AreaChart.Subtitle = chartData.Subtitle;
                        previewViewModel.AreaChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.AreaChart.Series = chartData.Series.MapTo<AreaChartDataViewModel.SeriesViewModel>();
                        previewViewModel.AreaChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "speedometer":
                    {
                        var chartData = _artifactServie.GetSpeedometerChartData(artifactResp.MapTo<GetSpeedometerChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.SpeedometerChart = new SpeedometerChartDataViewModel();
                        previewViewModel.SpeedometerChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.SpeedometerChart.Subtitle = chartData.Subtitle;
                        previewViewModel.SpeedometerChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "trafficlight":
                    {
                        var chartData = _artifactServie.GetTrafficLightChartData(artifactResp.MapTo<GetTrafficLightChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.TrafficLightChart = new TrafficLightChartDataViewModel();
                        previewViewModel.TrafficLightChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.TrafficLightChart.Subtitle = chartData.Subtitle;
                        previewViewModel.TrafficLightChart.ValueAxisTitle = artifactResp.Measurement;
                        previewViewModel.TrafficLightChart.Series = chartData.Series.MapTo<TrafficLightChartDataViewModel.SeriesViewModel>();
                        previewViewModel.TrafficLightChart.PlotBands = chartData.PlotBands.MapTo<TrafficLightChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "tabular":
                    {
                        var chartData = _artifactServie.GetTabularData(artifactResp.MapTo<GetTabularDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Tabular = new TabularDataViewModel();
                        chartData.MapPropertiesToInstance<TabularDataViewModel>(previewViewModel.Tabular);
                        previewViewModel.Tabular.Title = artifactResp.HeaderTitle;
                        //previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        //previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "tank":
                    {
                        var chartData = _artifactServie.GetTankData(artifactResp.MapTo<GetTankDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.Tank = new TankDataViewModel();
                        chartData.MapPropertiesToInstance<TankDataViewModel>(previewViewModel.Tank);
                        previewViewModel.Tank.Title = artifactResp.HeaderTitle;
                        previewViewModel.Tank.Subtitle = chartData.Subtitle;
                        //previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        //previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                default:
                    {
                        var chartData = _artifactServie.GetChartData(artifactResp.MapTo<GetCartesianChartDataRequest>());
                        previewViewModel.GraphicType = artifactResp.GraphicType;
                        previewViewModel.BarChart = new BarChartDataViewModel();
                        previewViewModel.BarChart.Title = artifactResp.HeaderTitle;
                        previewViewModel.BarChart.Subtitle = chartData.Subtitle;
                        previewViewModel.BarChart.ValueAxisTitle = artifactResp.Measurement; //.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.BarChart.Series = chartData.Series.MapTo<BarChartDataViewModel.SeriesViewModel>();
                        previewViewModel.BarChart.Periodes = chartData.Periodes;
                        previewViewModel.BarChart.SeriesType = chartData.SeriesType;
                    }
                    break;
            }
            return Json(previewViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Preview(ArtifactDesignerViewModel viewModel)
        {
            var previewViewModel = new ArtifactPreviewViewModel();
            switch (viewModel.GraphicType)
            {
                case "line":
                    {
                        var cartesianRequest = viewModel.MapTo<GetCartesianChartDataRequest>();
                        viewModel.LineChart.MapPropertiesToInstance<GetCartesianChartDataRequest>(cartesianRequest);
                        var chartData = _artifactServie.GetChartData(cartesianRequest);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.LineChart = new LineChartDataViewModel();
                        previewViewModel.LineChart.Title = viewModel.HeaderTitle;
                        previewViewModel.LineChart.Subtitle = chartData.Subtitle;
                        previewViewModel.LineChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.LineChart.Series = chartData.Series.MapTo<LineChartDataViewModel.SeriesViewModel>();
                        previewViewModel.LineChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "area":
                    {
                        var cartesianRequest = viewModel.MapTo<GetCartesianChartDataRequest>();
                        viewModel.AreaChart.MapPropertiesToInstance<GetCartesianChartDataRequest>(cartesianRequest);
                        var chartData = _artifactServie.GetChartData(cartesianRequest);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.AreaChart = new AreaChartDataViewModel();
                        previewViewModel.AreaChart.Title = viewModel.HeaderTitle;
                        previewViewModel.AreaChart.Subtitle = chartData.Subtitle;
                        previewViewModel.AreaChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.AreaChart.Series = chartData.Series.MapTo<AreaChartDataViewModel.SeriesViewModel>();
                        previewViewModel.AreaChart.Periodes = chartData.Periodes;
                    }
                    break;
                case "speedometer":
                    {
                        var request = viewModel.MapTo<GetSpeedometerChartDataRequest>();
                        viewModel.SpeedometerChart.MapPropertiesToInstance<GetSpeedometerChartDataRequest>(request);
                        var chartData = _artifactServie.GetSpeedometerChartData(request);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.SpeedometerChart = new SpeedometerChartDataViewModel();
                        previewViewModel.SpeedometerChart.Title = viewModel.HeaderTitle;
                        previewViewModel.SpeedometerChart.Subtitle = chartData.Subtitle;
                        previewViewModel.SpeedometerChart.ValueAxisTitle = _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId }).Name;
                        previewViewModel.SpeedometerChart.Series = chartData.Series.MapTo<SpeedometerChartDataViewModel.SeriesViewModel>();
                        previewViewModel.SpeedometerChart.PlotBands = chartData.PlotBands.MapTo<SpeedometerChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "trafficlight":
                    {
                        var request = viewModel.MapTo<GetTrafficLightChartDataRequest>();
                        viewModel.TrafficLightChart.MapPropertiesToInstance<GetTrafficLightChartDataRequest>(request);
                        var chartData = _artifactServie.GetTrafficLightChartData(request);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.TrafficLightChart = new TrafficLightChartDataViewModel();
                        previewViewModel.TrafficLightChart.Title = viewModel.HeaderTitle;
                        previewViewModel.TrafficLightChart.Subtitle = chartData.Subtitle;
                        previewViewModel.TrafficLightChart.ValueAxisTitle =
                            _measurementService.GetMeasurement(new GetMeasurementRequest { Id = viewModel.MeasurementId })
                                               .Name;
                        previewViewModel.TrafficLightChart.Series =
                            chartData.Series.MapTo<TrafficLightChartDataViewModel.SeriesViewModel>();
                        previewViewModel.TrafficLightChart.PlotBands =
                            chartData.PlotBands.MapTo<TrafficLightChartDataViewModel.PlotBandViewModel>();
                    }
                    break;
                case "tabular":
                    {
                        var request = viewModel.MapTo<GetTabularDataRequest>();
                        viewModel.Tabular.MapPropertiesToInstance<GetTabularDataRequest>(request);
                        var chartData = _artifactServie.GetTabularData(request);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.Tabular = new TabularDataViewModel();
                        chartData.MapPropertiesToInstance<TabularDataViewModel>(previewViewModel.Tabular);
                        previewViewModel.Tabular.Title = viewModel.HeaderTitle;
                    }
                    break;
                case "tank":
                    {
                        var request = viewModel.MapTo<GetTankDataRequest>();
                        //viewModel.Tank.MapPropertiesToInstance<GetTankDataRequest>(request);
                        var chartData = _artifactServie.GetTankData(request);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.Tank = new TankDataViewModel();
                        chartData.MapPropertiesToInstance<TankDataViewModel>(previewViewModel.Tank);
                        previewViewModel.Tank.Title = viewModel.HeaderTitle;
                        previewViewModel.Tank.Subtitle = chartData.Subtitle;
                    }
                    break;
                default:
                    {
                        var cartesianRequest = viewModel.MapTo<GetCartesianChartDataRequest>();
                        viewModel.BarChart.MapPropertiesToInstance<GetCartesianChartDataRequest>(cartesianRequest);
                        var chartData = _artifactServie.GetChartData(cartesianRequest);
                        previewViewModel.GraphicType = viewModel.GraphicType;
                        previewViewModel.BarChart = new BarChartDataViewModel();
                        previewViewModel.BarChart.Title = viewModel.HeaderTitle;
                        previewViewModel.BarChart.Subtitle = chartData.Subtitle;
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
        public ActionResult Create(ArtifactDesignerViewModel viewModel)
        {
            switch (viewModel.GraphicType)
            {
                case "line":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.LineChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;

                case "area":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.AreaChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
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

                case "trafficlight":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.TrafficLightChart.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;
                case "tabular":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        viewModel.Tabular.MapPropertiesToInstance<CreateArtifactRequest>(request);
                        _artifactServie.Create(request);
                    }
                    break;
                case "tank":
                    {
                        var request = viewModel.MapTo<CreateArtifactRequest>();
                        //viewModel.Tank.MapPropertiesToInstance<CreateArtifactRequest>(request);
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
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ArtifactDesignerViewModel viewModel)
        {
            switch (viewModel.GraphicType)
            {
                case "line":
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.LineChart.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;

                case "area":
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.AreaChart.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;
                case "speedometer":
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.SpeedometerChart.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;
                case "trafficlight":
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.TrafficLightChart.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;
                case "tank" :
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.Tank.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        request.Id = viewModel.Id;
                        _artifactServie.Update(request);
                    }
                    break;
                case "tabular":
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.Tabular.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;
                default:
                    {
                        var request = viewModel.MapTo<UpdateArtifactRequest>();
                        viewModel.BarChart.MapPropertiesToInstance<UpdateArtifactRequest>(request);
                        _artifactServie.Update(request);
                    }
                    break;
            }
            return RedirectToAction("Index");
        }

        private string ParseDateToString(PeriodeType periodeType, DateTime? date)
        {
            switch (periodeType)
            {
                case PeriodeType.Yearly:
                    return date.HasValue ? date.Value.ToString("yyyy", CultureInfo.InvariantCulture) : string.Empty;
                case PeriodeType.Monthly:
                    return date.HasValue ? date.Value.ToString("MM/yyyy", CultureInfo.InvariantCulture) : string.Empty;
                case PeriodeType.Weekly:
                    return date.HasValue ? date.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) : string.Empty;
                case PeriodeType.Daily:
                    return date.HasValue ? date.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) : string.Empty;
                case PeriodeType.Hourly:
                    return date.HasValue ? date.Value.ToString("MM/dd/yyyy  h:mm", CultureInfo.InvariantCulture) : string.Empty;
                default:
                    return string.Empty;
            }
        }
    }
}
