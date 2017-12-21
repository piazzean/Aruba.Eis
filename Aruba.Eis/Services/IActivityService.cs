using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aruba.Eis.Services
{
    public interface IActivityService
    {
        /// <summary>
        /// Search activities by filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IList<Activity>> Search(string filter = null);

        /// <summary>
        /// Find activity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Activity> Find(int? id);

        /// <summary>
        /// Create activity to the DB
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task Create(Activity activity);

        /// <summary>
        /// Save activity to the DB
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task Save(Activity activity);

        /// <summary>
        /// Remove activity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Remove(int id);
    }
}
