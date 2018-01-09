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
            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<ActivityEntity, Activity>();
            CreateMap<Activity, ActivityEntity>()
                .ForMember(dest => dest.Resources, opt => opt.Ignore());

            CreateMap<IdentityRole, Role>();
            CreateMap<Role, IdentityRole>();

            CreateMap<ActivityResourceEntity, ActivityResource>();
            CreateMap<ActivityResource, ActivityResourceEntity>()
                .ForMember(dest => dest.Activity, opt => opt.Ignore())
                .ForMember(dest => dest.ActivityId, opt => opt.Ignore());

            CreateMap<ScheduleEntity, Schedule>();
            CreateMap<Schedule, ScheduleEntity>()
                .ForMember(dest => dest.Resources, opt => opt.Ignore());

            CreateMap<ScheduleResourceEntity, ScheduleResource>();
            CreateMap<ScheduleResource, ScheduleResourceEntity>()
                .ForMember(dest => dest.Schedule, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore());
        }
    }
}