using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Services.Responses.Menu;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMenuService
    {
        GetMenuResponse GetMenu(GetMenuRequest request);
        GetMenusResponse GetMenus(GetMenusRequest request);
        CreateMenuResponse Create(CreateMenuRequest request);
        UpdateMenuResponse Update(UpdateMenuRequest request);
        DeleteMenuResponse Delete(int id);
    }
}
