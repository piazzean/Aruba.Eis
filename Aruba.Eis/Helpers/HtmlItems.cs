using Aruba.Eis.Context;
using Aruba.Eis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aruba.Eis.Helpers
{
    public static class HtmlItems
    {
        public static List<SelectListItem> RoleListItems { get; set; }

        public static async Task LoadRoleListItems()
        {
            RoleListItems = new List<SelectListItem>();
            var roleService = BeanFactory.GetInstance<IUserService>();
            var roles = await roleService.SearchRoles();
            foreach (var role in roles)
            {
                RoleListItems.Add(new SelectListItem()
                    {
                        Text = role.Name,
                        Value = role.Id
                    }
                );
            }
        }
    }
}