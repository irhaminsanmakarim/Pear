using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IUserService
    {
        GetUserResponse GetUser(GetUserRequest request);
        GetUsersResponse GetUsers(GetUsersRequest request);
    }
}
