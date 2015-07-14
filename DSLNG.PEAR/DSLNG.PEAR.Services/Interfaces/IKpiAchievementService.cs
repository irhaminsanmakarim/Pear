using DSLNG.PEAR.Services.Requests.KpiAchievement;
using DSLNG.PEAR.Services.Responses.KpiAchievement;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IKpiAchievementService
    {
        GetKpiAchievementsResponse GetKpiAchievements(GetKpiAchievementsRequest request);
        UpdateKpiAchievementsResponse UpdateKpiAchievements(UpdateKpiAchievementsRequest request);
        AllKpiAchievementsResponse GetAllKpiAchievements();
        GetKpiAchievementsConfigurationResponse GetKpiAchievementsConfiguration(GetKpiAchievementsConfigurationRequest request);
        UpdateKpiAchievementItemResponse UpdateKpiAchievementItem(UpdateKpiAchievementItemRequest request);
    }
}
