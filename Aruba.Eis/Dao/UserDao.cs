using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Entities;
using Aruba.Eis.EntityFramework;

namespace Aruba.Eis.Dao
{
    public class UserDao : BaseDao
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public UserDao(string username = null)
            : base(username)
        {
        }

        /// <summary>
        /// Search Users from DB
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<UserEntity>> Search(string filter = null)
        {
            try
            {
                using (var userStore = new UserStore<UserEntity>(new ApplicationDbContext()))
                using (var userMngr = new ApplicationUserManager(userStore))
                {
                    return await userMngr.Users.ToListAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
        
        /// <summary>
        /// Find user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<UserEntity> Find(string id)
        {
            try
            {
                using (var userStore = new UserStore<UserEntity>(new ApplicationDbContext()))
                using (var userMngr = new ApplicationUserManager(userStore))
                {
                    var query = userMngr.Users.Where(x => x.Id == id);
                    return await query.FirstOrDefaultAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Search Roles from DB
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<IdentityRole>> SearchRoles(string filter = null)
        {
            try
            {
                using (var roleStore = new RoleStore<IdentityRole>(_ctx))
                using (var roleMngr = new RoleManager<IdentityRole>(roleStore))
                {
                    return await roleMngr.Roles.ToListAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Find role by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IdentityRole> FindRole(string id)
        {
            try
            {
                using (var roleStore = new RoleStore<IdentityRole>(_ctx))
                using (var roleMngr = new RoleManager<IdentityRole>(roleStore))
                {
                    var query = roleMngr.Roles.Where(x => x.Id == id);
                    return await query.FirstOrDefaultAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Create a role to the DB
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task CreateRole(IdentityRole role)
        {
            try
            {
                using (var roleStore = new RoleStore<IdentityRole>(_ctx))
                {
                    await roleStore.CreateAsync(role);
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Remove role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveRole(string id)
        {
            try
            {
                using (var roleStore = new RoleStore<IdentityRole>(_ctx))
                using (var roleMngr = new RoleManager<IdentityRole>(roleStore))
                {
                    var ir = await FindRole(id);
                    if (ir != null)
                    {
                        await roleStore.DeleteAsync(ir);
                        await _ctx.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
    }
}