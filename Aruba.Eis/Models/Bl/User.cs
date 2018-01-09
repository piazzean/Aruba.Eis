using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aruba.Eis.Models.Bl
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public IList<Role> Roles { get; set; }
        
        public IList<Team> Teams { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}