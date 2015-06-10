using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DSLNG.PEAR.Model.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public string Username { get; set; }

        [Index(IsUnique = true)]
        public string Email { get; set; }
        public RoleGroup Role { get; set; }

        public bool IsActive { get; set; }
    }
}
