using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DynamicData.Models
{
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [AllowNull]
        public Guid GUID { get; set; }

        public string Username { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public bool Active { get; set; }
        public List<Role> Roles { get; set; }


        public List<UserRole> UserRole { get; set; }
        public List<Organization> Organization { get; set; }
        public List<UserOrganization> UserOrganization { get; set; }

        public User()
        {
            this.GUID = Guid.NewGuid();
        }
    }
}
