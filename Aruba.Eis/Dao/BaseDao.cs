using System;
using System.Data.Entity;
using System.Threading.Tasks;
using log4net;
using Aruba.Eis.EntityFramework;
using Aruba.Eis.Exceptions;

namespace Aruba.Eis.Dao
{
    public class BaseDao : IDisposable
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

        /// <summary>
        /// DB Transaction
        /// </summary>
        protected DbContextTransaction _tx;

        /// <summary>
        /// Class constructor
        /// </summary>
        public BaseDao(string username = null)
        {
            _ctx = new ApplicationDbContext(username);
            _tx = _ctx.Database.BeginTransaction();
        }

        /// <summary>
        /// Save Changes to the DB
        /// </summary>
        /// <returns></returns>
        public async Task SaveChanges()
        {
            try
            {
                await _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Commit the transaction
        /// </summary>
        /// <returns>The transaction object created</returns>
        public void CommitTransaction()
        {
            try
            {
                _tx.Commit();
            }
            catch (Exception e)
            {
                Log.Error(EisException.RepositoryError.Message, e);
                throw EisException.RepositoryError;
            }
        }

        /// <summary>
        /// Dispose objects
        /// </summary>
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}