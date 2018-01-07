using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aruba.Eis.Services
{
    public interface IScheduleService
    {
        /// <summary>
        /// Search schedules by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<Schedule>> Search(string filter = null);

        /// <summary>
        /// Find schedule by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Schedule> Find(int? id);

        /// <summary>
        /// Create schedule on the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        Task Create(Schedule schedule);

        /// <summary>
        /// Save schedule to the DB
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        Task Save(Schedule schedule);

        /// <summary>
        /// Remove schedule by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Remove(int id);
    }
}
