using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Models.Identity.ViewModels
{
    public class AssignRoleModel
    {
        [Required]
        [MinLength(2), MaxLength(20)]
        public string User { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
