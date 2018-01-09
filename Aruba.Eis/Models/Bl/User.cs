using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace Aruba.Eis.Models.Bl
{
    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public IList<Role> Roles { get; set; }
        
        public IList<Team> Teams { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }
}