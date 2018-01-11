using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using log4net;
using Aruba.Eis.Services;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Helpers;
using Aruba.Eis.Models.Views;

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
        private ApplicationUserManager UserManager { get; set; }

        /// <summary>
        /// Team Controller constructor
        /// </summary>
        public ScheduleController(IActivityService activityService, IScheduleService scheduleService, ApplicationUserManager userManager)
        {
            // DI
            ActivityService = activityService;
            ScheduleService = scheduleService;
            UserManager = userManager;
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
                schedule.Assignments = new List<Assignment>();
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
                schedule = await ScheduleService.Find(id);
            }
            
            await HtmlItems.LoadRoleListItems();
            
            return View(schedule);
        }

        //
        // POST: /Schedule/Details
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details([Bind(Include = "Id,Code,Name,StartDateString,EndDateString,Resources")] Schedule schedule)
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
        
        //
        // GET: /Schedule/Delete/5
        //
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await ScheduleService.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        //
        // POST: /Schedule/Delete/5
        //
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await ScheduleService.Remove(id);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Schedule/Assign/5
        //
        public async Task<ActionResult> Assign(int? id, int? scheduleId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assignment assignment;
            if (id == 0)
            {
                var schedule = await ScheduleService.Find(scheduleId);
                if (schedule == null)
                {
                    return HttpNotFound();
                }
                assignment = new Assignment();
                assignment.Schedule = schedule;
            }
            else
            {
                assignment = await ScheduleService.FindAssignment(id);
            }
            return View(assignment);
        }

        //
        // POST: /Schedule/Assign/5
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Assign(Assignment assignment)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            assignment.UserId = user.Id;
            await ScheduleService.CreateAssignment(assignment);
            return RedirectToAction("Details", "Schedule", new { id = assignment.ScheduleId });
        }
    }
}
