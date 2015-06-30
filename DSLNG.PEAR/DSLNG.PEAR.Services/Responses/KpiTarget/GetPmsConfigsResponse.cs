using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.KpiTarget
{
    public class GetPmsConfigsResponse : BaseResponse
    {
        public IList<PmsConfig> PmsConfigs { get; set; }

        public class PmsConfig
        {
            public Pillar Pillar { get; set; }
            public bool IsActive { get; set; }
            public ICollection<PmsConfigDetails> PmsConfigDetailsList { get; set; }
            public PmsSummary PmsSummary { get; set; }
        }

        public class Pillar
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class PmsSummary
        {
            public int Year { get; set; }
        }

        public class PmsConfigDetails
        {
            public Kpi Kpi { get; set; }
            public bool IsActive { get; set; }
        }

        public class Kpi
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Measurement Measurement { get; set; }
        }

        public class Measurement
        {
            public string Name { get; set; }
        }
    }
}
