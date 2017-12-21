using Aruba.Eis.Dao;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Models.Entities;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aruba.Eis.Services.Impl
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Search roles by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IList<Role>> SearchRoles(string filter = null)
        {
            using (var dao = new UserDao())
            {
                var irList = await dao.SearchRoles(filter);
                var rList = Mapper.Map<IList<IdentityRole>, IList<Role>>(irList);
                return rList;
            }
        }

        /// <summary>
        /// Find role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Role> FindRole(string id)
        {
            using (var dao = new UserDao())
            {
                var ir = await dao.FindRole(id);
                if (ir != null)
                    return Mapper.Map<IdentityRole, Role>(ir);
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Create role to the DB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task CreateRole(Role role)
        {
            using (var dao = new UserDao())
            {
                var ir = await dao.FindRole(role.Id);
                if (ir == null)
                {
                    ir = Mapper.Map<Role, IdentityRole>(role);
                    await dao.CreateRole(ir);
                    dao.CommitTransaction();
                }
                else
                    throw EisException.RecordAlreadyExists;
            }
        }

        /// <summary>
        /// Save role to the DB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task SaveRole(Role role)
        {
            using (var dao = new UserDao())
            {
                var ir = await dao.FindRole(role.Id);
                if (ir != null)
                {
                    Mapper.Map<Role, IdentityRole>(role, ir);
                    await dao.SaveChanges();
                    dao.CommitTransaction();
                }
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Remove role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveRole(string id)
        {
            using (var dao = new UserDao())
            {
                await dao.RemoveRole(id);
                dao.CommitTransaction();
            }
        }
    }
}