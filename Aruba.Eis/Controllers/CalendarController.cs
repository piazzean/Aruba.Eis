using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Aruba.Eis.Context;
using Aruba.Eis.Models.Bl;
using Aruba.Eis.Services;
using Aruba.Eis.Services.Impl;

namespace Aruba.Eis.Controllers
{
    [Authorize]
    [RoutePrefix("Calendar")]
    public class CalendarController : ApiController
    {
        // Dependencies
        private ApplicationUserManager UserManager { get; set; }
        private IScheduleService ScheduleService { get; set; }

        public CalendarController(
            // ApplicationUserManager userManager, IScheduleService scheduleService
            )
        {
            //this.UserManager = userManager;
            //this.ScheduleService = scheduleService;

            this.ScheduleService = BeanFactory.GetInstance<IScheduleService>();
        }
        
        [Route("getEvents")]
        [HttpGet]
        public async Task<IHttpActionResult> GetEvents(DateTime start, DateTime end)
        {
            var schedules = await ScheduleService.Search(start, end);

            var eventList = new List<ScheduleEvent>();
            
            foreach (var schedule in schedules)
            {
                var text = schedule.Name + "<br/><br/>";
                text += "AU:"+
                        " <span class='glyphicon glyphicon-check' aria-hidden='true'></span>"+
                        "<br/>";
                text += "SC:" + 
                        " <span class='glyphicon glyphicon-check' aria-hidden='true'></span>"+
                        " <span class='glyphicon glyphicon-unchecked' aria-hidden='true'></span>"+
                        " <span class='glyphicon glyphicon-unchecked' aria-hidden='true'></span>"+
                        "<br/>";
                
                var e = new ScheduleEvent()
                {
                    id = schedule.Id,
                    title = text, 
                    start = schedule.StartDateTime.ToString(@"yyyy-MM-dd HH:mm"),
                    end = schedule.EndDateTime.ToString(@"yyyy-MM-dd HH:mm"),
                    color = "#ff0000",
                    allDay = false
                };
                eventList.Add(e);
            }

            return Json(eventList);
        }
    }
}