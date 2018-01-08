using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aruba.Eis.EntityFramework;
using Aruba.Eis.Models.Entities;
using Aruba.Eis.Models.Views;
using log4net;
using Aruba.Eis.Services;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Helpers;

namespace Aruba.Eis.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        /// <summary>
        /// Log Manager
        /// </summary>
        private static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Dependencies
        /// </summary>
        private IActivityService ActivityService { get; set; }
        private IScheduleService ScheduleService { get; set; }

        /// <summary>
        /// Team Controller constructor
        /// </summary>
        public ScheduleController(IActivityService activityService, IScheduleService scheduleService)
        {
            // DI
            ActivityService = activityService;
            ScheduleService = scheduleService;
        }

        //
        // GET: /Schedule/Create
        //
        public async Task<ActionResult> Create()
        {
            await HtmlItems.LoadActivityListItems();
            return View();
        }

        //
        // POST: /Schedule/Create
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Activity")] ScheduleCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var activityId = viewModel.Activity;
                if (activityId != null)
                {
                    return RedirectToAction("Details", new { id = 0, activityId = int.Parse(activityId) });
                }
            }

            return View(viewModel);
        }
        
        //
        // GET: /Schedule/Details/5
        //
        public async Task<ActionResult> Details(int? id, int? activityId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Schedule schedule;

            if (id == 0)
            {
                schedule = new Schedule();
                var activity = await ActivityService.Find(activityId);
                schedule.Code = activity.Code;
                schedule.Name = activity.Name;
                schedule.StartDateTime = DateTime.Now;
                schedule.EndDateTime = DateTime.Now;
                schedule.Resources = new List<ScheduleResource>();
                foreach (var actres in activity.Resources)
                {
                    var schres = new ScheduleResource()
                    {
                        RoleId = actres.RoleId,
                        MinOccurs = actres.MinOccurs,
                        MaxOccurs = actres.MaxOccurs
                    };
                    schedule.Resources.Add(schres);
                }
            }
            else
            {
                schedule = new Schedule();
                // schedule = ScheduleService.Find(id);
            }
            
            await HtmlItems.LoadRoleListItems();
            
            return View(schedule);
        }

        //
        // POST: /Activity/Details
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details([Bind(Include = "Id,Code,Name,StartDateTime,EndDateTime,Resources")] Schedule schedule)
        {
            foreach (var key in ModelState.Keys)
            {
                if (key.EndsWith("Role.Name"))
                    ModelState[key].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                if (schedule.Id > 0)
                {
                    await ScheduleService.Save(schedule);
                }
                else
                {
                    await ScheduleService.Create(schedule);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(schedule);
        }

    }
}
