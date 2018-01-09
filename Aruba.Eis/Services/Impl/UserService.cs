using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using Aruba.Eis.Dao;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Models.Entities;

namespace Aruba.Eis.Services.Impl
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Search users by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IList<User>> Search(string filter = null)
        {
            using (var dao = new UserDao())
            {
                var ueList = await dao.Search(filter);
                var uList = Mapper.Map<IList<UserEntity>,IList<User>>(ueList);
                return uList;
            }
        }
        
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<User> Find(string id)
        {
            using (var dao = new UserDao())
            {
                var ue = await dao.Find(id);
                if (ue != null)
                {
                    var user = Mapper.Map<UserEntity, User>(ue);
                    user.Roles = await this.SearchRoles();
                    foreach (var ir in ue.Roles)
                    {
                        var role = user.Roles.FirstOrDefault(r => r.Id.Equals(ir.RoleId));
                        if (role != null)
                        {
                            role.Granted = true;
                        }
                    }
                    return user;
                }
                else
                    throw EisException.RecordNotFound;
            }
        }
        
        /// <summary>
        /// Create user on the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Create(User user)
        {
            using (var dao = new UserDao())
            {
                var ue = Mapper.Map<User, UserEntity>(user);
                await dao.Create(ue, user.Password);
                foreach (var role in user.Roles)
                {
                    await dao.UserRoleUpdate(ue, role.Name, role.Granted);
                }
            }
        }
        
        /// <summary>
        /// Save user to the DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Save(User user)
        {
            using (var dao = new UserDao())
            {
                var ue = await dao.Find(user.Id);
                if (ue != null)
                {
                    Mapper.Map<User, UserEntity>(user, ue);
                    foreach (var role in user.Roles)
                    {
                        await dao.UserRoleUpdate(ue, role.Name, role.Granted);
                    }
                    await dao.Update(ue);
                }
                else
                    throw EisException.RecordNotFound;
            }
        }
        
        /// <summary>
        /// Remove user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(string id)
        {
            using (var dao = new UserDao())
            {
                await dao.Remove(id);
            }
        }
        
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
            }
        }
    }
}