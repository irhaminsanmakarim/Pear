using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.KpiAchievement
{
    public class GetAchievementsResponse : BaseResponse
    {
        public GetAchievementsResponse()
        {
            KpiAchievements = new List<GetKpiAchievementResponse>();
        }
        public List<GetKpiAchievementResponse> KpiAchievements { get; set; }
    }
}
