using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Services.Responses.Menu;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IMenuService
    {
        GetMenuResponse GetMenus(GetMenuRequest request);
    }
}
