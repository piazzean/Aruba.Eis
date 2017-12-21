using Aruba.Eis.Models.Bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aruba.Eis.Services
{
    public interface ITeamService
    {
        Task<IList<Team>> Search(string filter); 
    }
}
