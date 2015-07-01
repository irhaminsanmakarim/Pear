using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Requests.User
{
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        //public RoleGroup Role { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
    }

    //public class RoleGroup
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Icon { get; set; }
    //    public string Remark { get; set; }
    //    public bool IsActive { get; set; }
    //}
}
