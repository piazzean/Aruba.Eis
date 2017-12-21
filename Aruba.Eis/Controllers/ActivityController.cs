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

namespace Aruba.Eis.Controllers
{
    [Authorize]
    public class ActivityController : Controller
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

        /// <summary>
        /// Team Controller constructor
        /// </summary>
        public ActivityController(IActivityService activityService)
        {
            // DI
            ActivityService = activityService;
        }

        //
        // GET: /Activity/Index
        //
        public async Task<ActionResult> Index()
        {
            var model = new ActivityIndexViewModel();
            model.ActivityList = await ActivityService.Search();
            return View(model);
        }

        //
        // POST: /Activity/Index
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(ActivityIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var filter = model.Filter;
                model.ActivityList = await ActivityService.Search(filter);
            }
            catch (Exception e)
            {
                Log.Error("Activity Index Error", e);
                ModelState.AddModelError("", "ERRORE!");
            }
            return View(model);
        }

        //
        // GET: /Activity/Create
        //
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Activity/Create
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Name")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                await ActivityService.Create(activity);
                return RedirectToAction("Index");
            }

            return View(activity);
        }

        //
        // GET: /Activity/Edit/5
        //
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var activity = await ActivityService.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Edit/5
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Name")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                await ActivityService.Save(activity);
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        //
        // GET: /Activity/Delete/5
        //
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = await ActivityService.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        //
        // POST: /Activity/Delete/5
        //
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await ActivityService.Remove(id);
            return RedirectToAction("Index");
        }

        //
        // GET: /Activity/AddResource
        //
        public PartialViewResult AddResource()
        {
            var model = new ActivityResource();
            return PartialView("_Resources", model);
        }
    }
}
