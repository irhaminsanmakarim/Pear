using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;

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
            var response = Mapper.Map<GetUserResponse>(user);
            
            return response;
        }

        public GetUsersResponse GetUsers(GetUsersRequest request)
        {
            var users = DataContext.Users.ToList();
            var response = new GetUsersResponse();
            response.Users = Mapper.Map(users, new List<UserResponse>());

            return response;
        }
    }
}
