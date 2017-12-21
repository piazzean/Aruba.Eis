using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Aruba.Eis.Services.Impl
{
    public class TeamService : ITeamService
    {
        public async Task<IList<Team>> Search(string filter)
        {
            var team = new Team()
            {
                Code = "T1",
                Name = "Team 1"
            };
            var teamList = new List<Team>();
            teamList.Add(team);
            return teamList;
        }
    }
}