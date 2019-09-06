using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Models.Identity.ViewModels
{
    public class RegisterAppUser
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        static public implicit operator AppUser(RegisterAppUser og)
        {
            return new AppUser()
            {
                UserName = og.Name,
                Email = og.Email,
            };
        }
    }
}
