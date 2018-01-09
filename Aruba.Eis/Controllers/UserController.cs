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
    public class UserController : Controller
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
        public UserController(IUserService userService)
        {
            // DI
            UserService = userService;
        }

        //
        // GET: /User/Index
        //
        public async Task<ActionResult> Index()
        {
            var model = new UserIndexViewModel();
            model.UserList = await UserService.Search();
            return View(model);
        }

        //
        // POST: /User/Index
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserIndexViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var filter = model.Filter;
                model.UserList = await UserService.Search(filter);
            }
            catch (Exception e)
            {
                Log.Error("User Index Error", e);
                ModelState.AddModelError("", "ERRORE!");
            }
            return View(model);
        }
        
        //
        // GET: /User/Edit/5
        //
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user;

            if (id.Equals("0"))
            {
                user = new User();
                user.Id = "0";
                user.Roles = await UserService.SearchRoles();
            }
            else
            {
                user = await UserService.Find(id);
            }
            
            return View(user);
        }
        
        //
        // POST: /Schedule/Edit
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id.Equals("0"))
                {
                    user.Id = Guid.NewGuid().ToString();
                    await UserService.Create(user);
                }
                else
                {
                    await UserService.Save(user);
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /User/Delete/5
        //
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserService.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /User/Delete/5
        //
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            await UserService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
