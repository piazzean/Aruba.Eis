using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aruba.Eis.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Search Roles by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<Role>> SearchRoles(string filter = null);

        /// <summary>
        /// Find role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Role> FindRole(string id);

        /// <summary>
        /// Create role to the DB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task CreateRole(Role role);

        /// <summary>
        /// Save role to the DB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        Task SaveRole(Role role);

        /// <summary>
        /// Remove role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task RemoveRole(string id);
    }
}
