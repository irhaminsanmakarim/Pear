using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Group;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.Conversion;
using DSLNG.PEAR.Services.Responses;

namespace DSLNG.PEAR.Services.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, GetUserResponse>()
                  .ForMember(x => x.RoleName, o => o.MapFrom(m => m.Role.Name));
            Mapper.CreateMap<User, UserResponse>();
            Mapper.CreateMap<Level, GetLevelResponse>();
            Mapper.CreateMap<Menu, GetMenuResponse.Menu>()
                //.ForMember(m => m.RoleGroups, o => o.MapFrom(m => m.RoleGroups.MapTo<GetMenuResponse.RoleGroup>()))
                .ForMember(m => m.Menus, o => o.MapFrom(m => m.Menus.MapTo<GetMenuResponse.Menu>()));
            Mapper.CreateMap<Level, GetMenuResponse.Level>();
            Mapper.CreateMap<RoleGroup, GetMenuResponse.RoleGroup>()
                .ForMember(m => m.Level, o => o.MapFrom(m => m.Level.MapTo<GetMenuResponse.Level>()));
            Mapper.CreateMap<Group, GetGroupResponse.Group>();
            Mapper.CreateMap<Activity, GetGroupResponse.Activity>();
            Mapper.CreateMap<Measurement, GetMeasurementResponse>();
            Mapper.CreateMap<Method, GetMethodResponse>();
            Mapper.CreateMap<Conversion, GetConversionResponse>();
            base.Configure();
        }
    }
}

