using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Entities;

namespace Aruba.Eis.Dao
{
    public class ActivityDao : BaseDao
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public ActivityDao(string username = null)
            : base(username)
        {
        }

        /// <summary>
        /// Search Activity from DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<ActivityEntity>> Search(string filter = null)
        {
            try
            {
                var query = _ctx.DbActivities.AsQueryable();
                if (filter != null)
                    query = query.Where(x => x.Code.Contains(filter) || x.Name.Contains(filter));
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Find activity by id
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<ActivityEntity> Find(int? id)
        {
            try
            {
                var query = _ctx.DbActivities.Where(x => x.Id == id);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Create an activity to the DB
        /// </summary>
        /// <param name="parameterEntity"></param>
        /// <returns></returns>
        public async Task Create(ActivityEntity activity)
        {
            try
            {
                _ctx.DbActivities.Add(activity);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Remove activity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(int id)
        {
            try
            {
                var ae = await Find(id);
                if (ae != null)
                {
                    _ctx.DbActivities.Remove(ae);
                    await _ctx.SaveChangesAsync();
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