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
    [Authorize(Roles = Role.Admin)]
    public class RoleController : Controller
    {
        /// <summary>
        /// Log Manager
        /// </summary>
        protected static readonly ILog Log =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // Dependencies
        private IUserService UserService { get; set; }

        /// <summary>
        /// Controller constructor
        /// </summary>
        public RoleController(IUserService userService)
        {
            // DI
            UserService = userService;
        }

        //
        // GET: /Role/Index
        //
        public async Task<ActionResult> Index()
        {
            var model = new RoleIndexViewModel();
            model.RoleList = await UserService.SearchRoles();
            return View(model);
        }

        //
        // POST: /Role/Index
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RoleIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var filter = model.Filter;
                model.RoleList = await UserService.SearchRoles(filter);
            }
            catch (Exception e)
            {
                Log.Error("Activity Index Error", e);
                ModelState.AddModelError("", "ERRORE!");
            }
            return View(model);
        }

        //
        // GET: /Role/Create
        //
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                await UserService.CreateRole(role);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        //
        // GET: /Role/Edit/5
        //
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await UserService.FindRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Edit/5
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] Role role)
        {
            if (ModelState.IsValid)
            {
                await UserService.SaveRole(role);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Role/Delete/5
        //
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = await UserService.FindRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        //
        // POST: /Role/Delete/5
        //
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await UserService.RemoveRole(id);
            return RedirectToAction("Index");
        }
    }
}
