using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Entities;

namespace Aruba.Eis.Dao
{
    public class ScheduleDao : BaseDao
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public ScheduleDao(string username = null)
            : base(username)
        {
        }

        /// <summary>
        /// Search Schedules from DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleEntity>> Search(string filter = null)
        {
            try
            {
                var query = _ctx.DbSchedules.AsQueryable();
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
        /// Find schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ScheduleEntity> Find(int? id)
        {
            try
            {
                var query = _ctx.DbSchedules.Where(x => x.Id == id);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Create a schedule on the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task Create(ScheduleEntity schedule)
        {
            try
            {
                _ctx.DbSchedules.Add(schedule);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Remove schedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(int id)
        {
            try
            {
                var se = await Find(id);
                if (se != null)
                {
                    _ctx.DbSchedules.Remove(se);
                    await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Remove scheduleresource
        /// </summary>
        /// <param name="se"></param>
        /// <returns></returns>
        public async Task RemoveResource(ScheduleResourceEntity se)
        {
            try
            {
                _ctx.DbScheduleResources.Remove(se);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }
    }
}