﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Aruba.Eis.Models.Bl;

namespace Aruba.Eis.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Search Users by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<User>> Search(string filter = null);

        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> Find(string id);
        
        /// <summary>
        /// Create user on the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Create(User user);
        
        /// <summary>
        /// Save user to the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task Save(User user);
        
        /// <summary>
        /// Remove user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Remove(string id);
        
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
