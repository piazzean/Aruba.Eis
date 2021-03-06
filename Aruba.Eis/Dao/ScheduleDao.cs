﻿using System;
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
        /// Search Schedules between start and end
        /// </summary>
        /// <returns></returns>
        public async Task<List<ScheduleEntity>> Search(DateTime start, DateTime end)
        {
            try
            {
                var query = _ctx.DbSchedules.Where(x => x.StartDateTime.CompareTo(end) < 0 &&
                                                        x.EndDateTime.CompareTo(start) > 0);
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

        /// <summary>
        /// Find assignment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AssignmentEntity> FindAssignment(int? id)
        {
            try
            {
                var query = _ctx.DbAssignments.Where(x => x.Id == id);
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Create an assignment on the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task CreateAssignment(AssignmentEntity assignment)
        {
            try
            {
                _ctx.DbAssignments.Add(assignment);
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