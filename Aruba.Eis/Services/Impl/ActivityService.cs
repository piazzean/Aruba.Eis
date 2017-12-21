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
    public class ActivityService : IActivityService
    {
        /// <summary>
        /// Search activities by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IList<Activity>> Search(string filter = null)
        {
            using (var dao = new ActivityDao())
            {
                var aeList = await dao.Search(filter);
                var aList = Mapper.Map<IList<ActivityEntity>, IList<Activity>>(aeList);
                return aList;
            }
        }

        /// <summary>
        /// Find activity by id
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<Activity> Find(int? id)
        {
            using (var dao = new ActivityDao())
            {
                var ae = await dao.Find(id);
                if (ae != null)
                    return Mapper.Map<ActivityEntity, Activity>(ae);
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Create activity to the DB
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task Create(Activity activity)
        {
            using (var dao = new ActivityDao())
            {
                var ae = Mapper.Map<Activity, ActivityEntity>(activity);
                await dao.Create(ae);
                dao.CommitTransaction();
            }
        }

        /// <summary>
        /// Save activity to the DB
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public async Task Save(Activity activity)
        {
            using (var dao = new ActivityDao())
            {
                var ae = await dao.Find(activity.Id);
                if (ae != null)
                {
                    Mapper.Map<Activity, ActivityEntity>(activity, ae);
                    await dao.SaveChanges();
                    dao.CommitTransaction();
                }
                else
                    throw EisException.RecordNotFound;
            }
        }

        /// <summary>
        /// Remove activity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task Remove(int id)
        {
            using (var dao = new ActivityDao())
            {
                await dao.Remove(id);
                dao.CommitTransaction();
            }
        }
    }
}