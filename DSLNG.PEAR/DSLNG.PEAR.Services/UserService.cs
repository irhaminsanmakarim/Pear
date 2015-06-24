using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;


namespace DSLNG.PEAR.Services
{
    public class UserService : BaseService, IUserService 
    {
        public UserService(IDataContext dataContext) : base(dataContext)
        {
        }

        public GetUsersResponse GetUsers(GetUsersRequest request)
        {
            var users = DataContext.Users.ToList();
            var response = new GetUsersResponse();
            response.Users = users.MapTo<GetUsersResponse.User>();

            return response;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            try
            {
                var user = DataContext.Users.First(x => x.Id == request.Id);
                var response = user.MapTo<GetUserResponse>(); //Mapper.Map<GetUserResponse>(user);

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetUserResponse
                    {
                        IsSuccess = false,
                        Message = x.Message
                    };
            }
        }

        public CreateUserResponse Create(CreateUserRequest request)
        {
            var response = new CreateUserResponse();
            try
            {
                var user = request.MapTo<User>();
                DataContext.Users.Add(user);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "User item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateUserResponse Update(UpdateUserRequest request)
        {
            var response = new UpdateUserResponse();
            try
            {
                var user = request.MapTo<User>();
                DataContext.Users.Attach(user);
                DataContext.Entry(user).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "User item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeleteUserResponse Delete(int id)
        {
            var response = new DeleteUserResponse();
            try
            {
                var user = new User { Id = id };
                DataContext.Users.Attach(user);
                DataContext.Entry(user).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "User item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
