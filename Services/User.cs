using System.Collections;
using System.Collections.Generic;
using System.Web.Providers.Entities;

namespace WarbandOfTheSpiritborn.Services
{
    public class Role
    { 
        public string Admin { get; set; } = "admin";
        public string User { get; set; } = "user";
        public string Guest { get; set; } = "guest";
    }
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
