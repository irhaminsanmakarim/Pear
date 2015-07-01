using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.PmsSummary
{
    public class GetPmsConfigDetailsResponse : BaseResponse
    {
        public int Id { get; set; }
        public IList<Pillar> Pillars { get; set; }
        public IList<Kpi> Kpis { get; set; }
        public IList<ScoreIndicator> ScoreIndicators { get; set; }
        public double Weight { get; set; }
        public string ScoringType { get; set; }
        public int PillarId { get; set; }
        public int KpiId { get; set; }

        public class Pillar
        {
            public string Name { get; set; }
            public int Id { get; set; }
        }

        public class Kpi 
        {
            public string Name { get; set; }
            public int Id { get; set; }
            public int PillarId { get; set; }
        }

        public class ScoreIndicator
        {
            public string Expression { get; set; }
            public string Color { get; set; }
        }
        
    }
}
