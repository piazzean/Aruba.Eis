using Aruba.Eis.Dao;
using Aruba.Eis.Exceptions;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aruba.Eis.Services.Impl
{
    public class ScheduleService : IScheduleService
    {
        /// Dependencies
        private IUserService UserService { get; set; }

        /// <summary>
        /// Object Constructor
        /// </summary>
        /// <param name="userService"></param>
        public ScheduleService(IUserService userService)
        {
            // DI
            UserService = userService;
        }

        /// <summary>
        /// Search schedules between start and end
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<IList<Schedule>> Search(DateTime start, DateTime end)
        {
            using (var dao = new ScheduleDao())
            {
                var seList = await dao.Search(start, end);
                var sList = Mapper.Map<IList<ScheduleEntity>, IList<Schedule>>(seList);
                return sList;
            }
        }

        /// <summary>
        /// Find schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Schedule> Find(int? id)
        {
            using (var dao = new ScheduleDao())
            {
                var se = await dao.Find(id);
                if (se != null)
                {
                    var schedule = Mapper.Map<ScheduleEntity, Schedule>(se);
                    foreach (var assignment in schedule.Assignments)
                    {
                        assignment.User = await UserService.Find(assignment.UserId);
                    }
                    return schedule;
                }
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Create a schedule to the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task Create(Schedule schedule)
        {
            using (var dao = new ScheduleDao())
            {
                var se = Mapper.Map<Schedule, ScheduleEntity>(schedule);
                foreach (var res in schedule.Resources)
                {
                    var sre = Mapper.Map<ScheduleResource, ScheduleResourceEntity>(res);
                    sre.Schedule = se;
                    sre.ScheduleId = se.Id;
                    se.Resources.Add(sre);
                }
                await dao.Create(se);
                dao.CommitTransaction();
            }
        }

        /// <summary>
        /// Save schedule to the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task Save(Schedule schedule)
        {
            using (var dao = new ScheduleDao())
            {
                var se = await dao.Find(schedule.Id);
                if (se != null)
                {
                    Mapper.Map<Schedule, ScheduleEntity>(schedule, se);
                    if (schedule.Resources != null)
                    {
                        // Update/Delete existing Resources
                        foreach (var res in se.Resources.ToList())
                        {
                            var newres = schedule.Resources.FirstOrDefault(r => r.Id == res.Id);
                            if (newres != null)
                            {
                                Mapper.Map(newres, res);
                            }
                            else
                            {
                                await dao.RemoveResource(res);
                            }
                        }
                        // Add new Resources
                        foreach (var newres in schedule.Resources)
                        {
                            if (newres.Id == 0)
                            {
                                var res = new ScheduleResourceEntity();
                                Mapper.Map(newres, res);
                                se.Resources.Add(res);
                            }
                        }
                    }
                    await dao.SaveChanges();
                    dao.CommitTransaction();
                }
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Remove schedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(int id)
        {
            using (var dao = new ScheduleDao())
            {
                await dao.Remove(id);
                dao.CommitTransaction();
            }
        }

        /// <summary>
        /// Find assignment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Assignment> FindAssignment(int? id)
        {
            using (var dao = new ScheduleDao())
            {
                var se = await dao.FindAssignment(id);
                if (se != null)
                    return Mapper.Map<AssignmentEntity, Assignment>(se);
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Create an assignment on the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public async Task CreateAssignment(Assignment assignment)
        {
            using (var dao = new ScheduleDao())
            using (var userDao = new UserDao())
            {
                var ae = Mapper.Map<Assignment, AssignmentEntity>(assignment);
                // ae.Schedule = await dao.Find(ae.ScheduleId);
                // ae.User = await userDao.Find(ae.UserId);

                await dao.CreateAssignment(ae);
                dao.CommitTransaction();
            }
        }
    }
}