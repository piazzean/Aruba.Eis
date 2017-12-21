using Aruba.Eis.Models.Views;
using Aruba.Eis.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Aruba.Eis.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {
        /// <summary>
        /// Log Manager
        /// </summary>
        protected static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Dependencies
        /// </summary>
        protected ITeamService TeamService { get; set; }

        /// <summary>
        /// Team Controller constructor
        /// </summary>
        public TeamController(ITeamService teamService)
        {
            // DI
            TeamService = teamService;
        }

        //
        // GET: /Team/Index
        //
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Team/Index
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(TeamIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var filter = model.Filter;
                model.TeamList = await TeamService.Search(filter);
            }
            catch (Exception e)
            {
                Log.Error("Team Index Error", e);
                ModelState.AddModelError("", "ERRORE!");
            }
            return View(model);
        }
    }
}
