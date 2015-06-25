using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Group;
using DSLNG.PEAR.Services.Responses.Kpi;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.Conversion;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;
using DSLNG.PEAR.Services.Responses.Pillar;
using DSLNG.PEAR.Services.Requests.Pillar;

namespace DSLNG.PEAR.Services.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, GetUserResponse>()
                  .ForMember(x => x.RoleName, o => o.MapFrom(m => m.Role.Name));
            Mapper.CreateMap<User, UserResponse>();
            Mapper.CreateMap<User, GetUsersResponse.User>();
            Mapper.CreateMap<CreateUserRequest, User>();
            Mapper.CreateMap<UpdateUserRequest, User>();
            Mapper.CreateMap<User, GetUserResponse>();
            Mapper.CreateMap<User, GetUsersResponse.User>();
            Mapper.CreateMap<GetUserRequest, User>();
            /*Level*/
            Mapper.CreateMap<Level, GetLevelsResponse.Level>();
            Mapper.CreateMap<Level, GetLevelResponse>();
            Mapper.CreateMap<CreateLevelRequest, Level>();
            Mapper.CreateMap<UpdateLevelRequest, Level>();
            Mapper.CreateMap<Level, UpdateLevelResponse>();


            Mapper.CreateMap<Menu, GetMenuResponse.Menu>()
                //.ForMember(m => m.RoleGroups, o => o.MapFrom(m => m.RoleGroups.MapTo<GetMenuResponse.RoleGroup>()))
                .ForMember(m => m.Menus, o => o.MapFrom(m => m.Menus.MapTo<GetMenuResponse.Menu>()));
            Mapper.CreateMap<CreateMenuRequest, Menu>();
            Mapper.CreateMap<Menu, GetMenusResponse.Menu>();
            Mapper.CreateMap<Menu, GetMenuResponse>();
            Mapper.CreateMap<UpdateMenuRequest, Menu>();

            Mapper.CreateMap<Level, GetMenuResponse.Level>();
            Mapper.CreateMap<RoleGroup, GetMenuResponse.RoleGroup>()
                .ForMember(m => m.Level, o => o.MapFrom(m => m.Level.MapTo<GetMenuResponse.Level>()));
            Mapper.CreateMap<Group, GetGroupResponse.Group>();
            Mapper.CreateMap<Activity, GetGroupResponse.Activity>();


            Mapper.CreateMap<Measurement, GetMeasurementsResponse>();
            Mapper.CreateMap<CreateMeasurementRequest, Measurement>();
            Mapper.CreateMap<UpdateMeasurementRequest, Measurement>();
            Mapper.CreateMap<Measurement, UpdateMeasurementResponse>();
            Mapper.CreateMap<Measurement, GetMeasurementResponse>();
            Mapper.CreateMap<GetMeasurementRequest, Measurement>();
            Mapper.CreateMap<Measurement, GetMeasurementsResponse.Measurement>();

            Mapper.CreateMap<Kpi, GetKpiToSeriesResponse.Kpi>();
            Mapper.CreateMap<Kpi, GetKpisResponse.Kpi>();

            Mapper.CreateMap<Measurement, GetMeasurementsResponse>();
            Mapper.CreateMap<Data.Entities.Method, GetMethodResponse>();

            Mapper.CreateMap<Conversion, GetConversionResponse>();

            Mapper.CreateMap<RoleGroup, GetRoleGroupsResponse.RoleGroup>();
            Mapper.CreateMap<RoleGroup, GetRoleGroupsResponse>();
            Mapper.CreateMap<RoleGroup, GetRoleGroupResponse>();
            Mapper.CreateMap<CreateRoleGroupRequest, RoleGroup>();
            Mapper.CreateMap<UpdateRoleGroupRequest, RoleGroup>();
            Mapper.CreateMap<RoleGroup, UpdateRoleGroupResponse>();

            Mapper.CreateMap<Type, GetTypeResponse>();
            Mapper.CreateMap<Type, GetTypesResponse>();
            Mapper.CreateMap<Type, GetTypesResponse.Type>();
            Mapper.CreateMap<CreateTypeRequest, Type>();
            Mapper.CreateMap<UpdateTypeRequest, Type>();
            Mapper.CreateMap<Type, UpdateTypeResponse>();

            Mapper.CreateMap<Pillar, GetPillarsResponse>();
            Mapper.CreateMap<Pillar, GetPillarResponse>();
            Mapper.CreateMap<Pillar, GetPillarsResponse.Pillar>();
            Mapper.CreateMap<CreatePillarRequest, Pillar>();
            Mapper.CreateMap<UpdatePillarRequest, Pillar>();

            base.Configure();
        }
    }
}

