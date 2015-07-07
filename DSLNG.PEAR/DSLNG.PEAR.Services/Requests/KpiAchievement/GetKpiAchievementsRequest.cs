using DSLNG.PEAR.Data.Enums;

namespace DSLNG.PEAR.Services.Requests.KpiAchievement
{
    public class GetKpiAchievementsRequest
    {
        public int PmsSummaryId { get; set; }
        public PeriodeType PeriodeType { get; set; }
    }
}
