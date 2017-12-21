using Aruba.Eis.Models.Bl;
using Aruba.Eis.Models.Entities;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aruba.Eis.Automapper
{
    public class EisMapperProfile : Profile
    {
        public EisMapperProfile()
        {
            CreateMap<ActivityEntity, Activity>();
            CreateMap<Activity, ActivityEntity>()
                .ForMember(dest => dest.Resources, opt => opt.Ignore());

            CreateMap<IdentityRole, Role>();
            CreateMap<Role, IdentityRole>();

        }
    }
}