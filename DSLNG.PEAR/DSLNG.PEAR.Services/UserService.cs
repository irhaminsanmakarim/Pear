using System.Linq;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;
using StructureMap;

namespace DSLNG.PEAR.Services
{
    public class UserService : BaseService, IUserService 
    {
        public UserService(IDataContext dataContext) : base(dataContext)
        {
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            var user = DataContext.Users.First(x => x.Id == request.Id);
            var response = new GetUserResponse
                {
                    Email = user.Email,
                    Id = user.Id,
                    Username = user.Username
                };

            return response;
        }
    }
}
