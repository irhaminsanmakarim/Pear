
using DevExpress.Web;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Web.DependencyResolution;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Web.ViewModels.Kpi;

namespace DSLNG.PEAR.Web.DevExpressDataProvider
{
    public static class ArtifactDesignerDataProvider
    {
        //private static IKpiService _kpiService = ObjectFactory.Container.GetInstance<IKpiService>();

        //public static object GetKpiRange(ListEditItemsRequestedByFilterConditionEventArgs args)
        //{
        //    var skip = args.BeginIndex;
        //    var take = args.EndIndex - args.BeginIndex + 1;
        //    return _kpiService.GetKpiToSeries(new GetKpiToSeriesRequest
        //    {
        //        Skip = skip,
        //        Take = take
        //    }).KpiList.MapTo<KpiToSeriesViewModel>();
        //}
        //public static object GetKpiById(ListEditItemRequestedByValueEventArgs args)
        //{
        //    int id;
        //    if (args.Value == null || !int.TryParse(args.Value.ToString(), out id))
        //        return null;
        //    return _kpiService.GetBy(new GetKpiRequest {
        //        Id = id
        //    }).MapTo<KpiToSeriesViewModel>();
        //}
    }
}