using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Aruba.Eis.Models;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Exceptions;

namespace Aruba.Eis.Controllers
{
    //[Authorize]
    [RoutePrefix("ActivityResource")]
    public class ActivityResourceController : ApiController
    {
        [HttpPost]
        [Route("Update")]
        public async Task<IHttpActionResult> Update(ActivityResource activityResource)
        {
            try
            {

                return Json(activityResource);
            }
            catch (EisException e)
            {
                return BadRequest();
            }
        }
    }
}