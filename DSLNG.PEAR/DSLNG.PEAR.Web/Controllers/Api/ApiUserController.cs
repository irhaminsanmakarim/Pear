using DSLNG.PEAR.Services;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Web.ViewModels;
using DSLNG.PEAR.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DSLNG.PEAR.Web.Controllers.Api
{
    public class ApiUserController : ApiController
    {
        private readonly IUserService _userService;
        public ApiUserController(IUserService userService)
        {
            _userService = userService;
        }

        //// GET api/apiuser
        //public UserIndexViewModel Get()
        //{
        //    var users = _userService.GetUsers(new GetUsersRequest());
        //    var viewModel = new UserIndexViewModel() { Users = users.Users.Select(x => new UserViewModel { Email = x.Email, Id = x.Id, Username = x.Username }) };
        //    return viewModel;
        //}

        // GET api/apiuser/5
        public IHttpActionResult Get(int id)
        {
            var user = _userService.GetUser(new GetUserRequest() {Id = id});
            //return Ok(user);
            if (user.IsSuccess != false) { 
                var viewModel = new UserViewModel() { Email = user.Email, Id = user.Id, Username = user.Username };
                return Ok(user);
            }else{
                return NotFound();
            }

            
        }

        // POST api/apiuser
        public void Post([FromBody]string value)
        {
        }

        // PUT api/apiuser/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/apiuser/5
        public void Delete(int id)
        {
        }
    }
}
