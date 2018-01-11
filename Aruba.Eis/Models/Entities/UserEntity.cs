using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using Aruba.Eis.Models.Entities;

namespace Aruba.Eis.Models.Entities
{
    public class UserEntity : IdentityUser
    {
        public UserEntity()
        {
            this.TeamResources = new HashSet<TeamResourceEntity>();
            this.Assignments = new HashSet<AssignmentEntity>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public virtual ICollection<TeamResourceEntity> TeamResources { get; set; }

        public virtual ICollection<AssignmentEntity> Assignments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserEntity> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}