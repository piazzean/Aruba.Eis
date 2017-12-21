using AutoMapper;
using Aruba.Eis.Automapper;

namespace Aruba.Eis.Services.Impl
{
    public class MapperService : IMapperService
    {
        public void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<EisMapperProfile>();
            });
        }
    }
}