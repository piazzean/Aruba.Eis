using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Aruba.Eis.Models.Bl;

namespace Aruba.Eis.Models.Views
{
    public class UserIndexViewModel
    {
        [Display(Name = "FILTER", ResourceType = typeof(I18nResource))]
        public string Filter { get; set; }

        public IList<User> UserList;

        public UserIndexViewModel()
        {
            UserList = new List<User>();
        }
    }
}