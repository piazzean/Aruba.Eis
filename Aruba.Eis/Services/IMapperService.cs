using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aruba.Eis.Services
{
    public interface IMapperService
    {
        /// <summary>
        /// Initialize Automapper transformations
        /// </summary>
        void Initialize();
    }
}
