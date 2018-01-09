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
using log4net;

namespace Aruba.Eis.Dao
{
    public class UserDao : IDisposable
    {
        /// <summary>
        /// Log manager
        /// </summary>
        protected static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// Entity Framework Context
        /// </summary>
        protected ApplicationDbContext _ctx;
        
        private UserStore<UserEntity> _userStore;
        private ApplicationUserManager _userMngr;
        
        /// <summary>
        /// Class constructor
        /// </summary>
        public UserDao(string username = null)
        {
            _ctx = new ApplicationDbContext(username);
            _userStore = new UserStore<UserEntity>(_ctx);
            _userMngr = new ApplicationUserManager(_userStore);
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
                return await _userMngr.Users.ToListAsync();
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
                var query = _userMngr.Users.Where(x => x.Id == id);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
        
        /// <summary>
        /// Create an user on the DB
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task Create(UserEntity user, string password)
        {
            try
            {
                await _userMngr.CreateAsync(user, password);
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
    
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task Update(UserEntity user)
        {
            try
            {
                await _userMngr.UpdateAsync(user);
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
        
        /// <summary>
        /// Update UserRole Relationship
        /// </summary>
        /// <param name="ue"></param>
        /// <param name="role"></param>
        /// <param name="granted"></param>
        /// <returns></returns>
        /// <exception cref="EisException"></exception>
        public async Task UserRoleUpdate(UserEntity ue, string role, bool granted)
        {
            try
            {
                if (granted)
                    await _userMngr.AddToRoleAsync(ue.Id, role);
                else
                    await _userMngr.RemoveFromRoleAsync(ue.Id, role);
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
        
        /// <summary>
        /// Remove user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(string id)
        {
            try
            {
                var user = await this.Find(id);
                await _userMngr.DeleteAsync(user);
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
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
        
        public void Dispose()
        {
            _ctx?.Dispose();
            _userMngr?.Dispose();
            _userStore?.Dispose();
        }
    }
}