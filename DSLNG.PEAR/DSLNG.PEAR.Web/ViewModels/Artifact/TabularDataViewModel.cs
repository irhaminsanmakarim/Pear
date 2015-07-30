

using System.Collections.Generic;
namespace DSLNG.PEAR.Web.ViewModels.Artifact
{
    public class TabularDataViewModel
    {
        public TabularDataViewModel()
        {
            Rows = new List<RowViewModel>();
        }
        public string Title { get; set; }
        public bool Actual { get; set; }
        public bool Target { get; set; }
        public bool Economic { get; set; }
        public bool Fullfillment { get; set; }
        public bool Remark { get; set; }

        public IList<RowViewModel> Rows { get; set; }
        public class RowViewModel {
            public string KpiName { get; set; }
            public string PeriodeType { get; set; }
            public string Periode { get; set; }
            public double? Actual { get; set; }
            public double? Target { get; set; }
            public string Remark { get; set; }
            public string Measurement { get; set; }
        }
    }
}