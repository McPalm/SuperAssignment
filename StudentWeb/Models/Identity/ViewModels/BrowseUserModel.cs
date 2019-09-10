using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Models.Identity.ViewModels
{
    public class BrowseUserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        static public implicit operator BrowseUserModel(IdentityUser user)
        {
            return new BrowseUserModel()
            {
                Name = user.UserName,
                Email = user.Email,
            };
        }
    }
}
