using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebApplication2.ModelView
{
    public class RoleViewModel
    {
        public string UsersID { get; set; }
        public string UsersEmail { get; set; }
        public IList<IdentityRole> Roles { get; set; }
        public IList<string> UsersRoles { get; set; }
        public RoleViewModel(){ // constructor values
            Roles = new List<IdentityRole>();
            UsersRoles = new List<string>();
        }
    }
}
