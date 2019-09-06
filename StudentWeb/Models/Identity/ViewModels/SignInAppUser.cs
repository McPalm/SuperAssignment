using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Models.Identity.ViewModels
{
    public class SignInAppUser
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
