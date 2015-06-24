using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsActive { get; set; }
        //[Required]
        //public string ChangeModel { get; set; }
    }
}