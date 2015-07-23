

using DSLNG.PEAR.Data.Enums;
using System;
using System.Collections.Generic;
namespace DSLNG.PEAR.Services.Requests.Artifact
{
    public class GetTabularDataRequest
    {
        public string HeaderTitle { get; set; }
        public bool Actual { get; set; }
        public bool Target { get; set; }
        public bool Economic { get; set; }
        public bool Fullfillment { get; set; }
        public bool Remark { get; set; }

        public IList<RowRequest> Rows { get; set; }

        public class RowRequest
        {
            public int KpiId { get; set; }
            public string KpiName { get; set; }
            public PeriodeType PeriodeType { get; set; }
            public RangeFilter RangeFilter { get; set; }
            public DateTime? Start { get; set; }
            public DateTime? End { get; set; }
        }
    }
}
