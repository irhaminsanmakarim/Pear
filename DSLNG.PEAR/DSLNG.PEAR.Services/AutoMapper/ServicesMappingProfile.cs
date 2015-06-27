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
using DSLNG.PEAR.Services.Responses.PmsConfigDetails;
using DSLNG.PEAR.Services.Responses.Pillar;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Requests.Kpi;

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
            Mapper.CreateMap<Data.Entities.Level, GetLevelsResponse.Level>();
            Mapper.CreateMap<Data.Entities.Level, GetLevelResponse>();
            Mapper.CreateMap<CreateLevelRequest, Data.Entities.Level>();
            Mapper.CreateMap<UpdateLevelRequest, Data.Entities.Level>();
            Mapper.CreateMap<Data.Entities.Level, UpdateLevelResponse>();


            Mapper.CreateMap<Menu, GetMenuResponse.Menu>()
                //.ForMember(m => m.RoleGroups, o => o.MapFrom(m => m.RoleGroups.MapTo<GetMenuResponse.RoleGroup>()))
                .ForMember(m => m.Menus, o => o.MapFrom(m => m.Menus.MapTo<GetMenuResponse.Menu>()));
            Mapper.CreateMap<CreateMenuRequest, Menu>();
            Mapper.CreateMap<Menu, GetMenusResponse.Menu>();
            Mapper.CreateMap<Menu, GetMenuResponse>();
            Mapper.CreateMap<UpdateMenuRequest, Menu>();

            Mapper.CreateMap<Data.Entities.Level, GetMenuResponse.Level>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetMenuResponse.RoleGroup>()
                .ForMember(m => m.Level, o => o.MapFrom(m => m.Level.MapTo<GetMenuResponse.Level>()));
            Mapper.CreateMap<Data.Entities.Group, GetGroupResponse.Group>();
            Mapper.CreateMap<Activity, GetGroupResponse.Activity>();


            Mapper.CreateMap<Data.Entities.Measurement, GetMeasurementsResponse>();
            Mapper.CreateMap<CreateMeasurementRequest, Data.Entities.Measurement>();
            Mapper.CreateMap<UpdateMeasurementRequest, Data.Entities.Measurement>();
            Mapper.CreateMap<Data.Entities.Measurement, UpdateMeasurementResponse>();
            Mapper.CreateMap<Data.Entities.Measurement, GetMeasurementResponse>();
            Mapper.CreateMap<GetMeasurementRequest, Data.Entities.Measurement>();
            Mapper.CreateMap<Data.Entities.Measurement, GetMeasurementsResponse.Measurement>();

            Mapper.CreateMap<Kpi, GetKpiToSeriesResponse.Kpi>();
            Mapper.CreateMap<Kpi, GetKpisResponse.Kpi>();
            Mapper.CreateMap<CreateKpiRequest, Kpi>();
            Mapper.CreateMap<Kpi, GetKpiResponse>();
            Mapper.CreateMap<UpdateKpiRequest, Kpi>();

            Mapper.CreateMap<Data.Entities.Measurement, GetMeasurementsResponse>();
            Mapper.CreateMap<Data.Entities.Method, GetMethodResponse>();

            Mapper.CreateMap<Data.Entities.Conversion, GetConversionResponse>();

            Mapper.CreateMap<Data.Entities.RoleGroup, GetRoleGroupsResponse.RoleGroup>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetRoleGroupsResponse>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetRoleGroupResponse>();
            Mapper.CreateMap<CreateRoleGroupRequest, Data.Entities.RoleGroup>();
            Mapper.CreateMap<UpdateRoleGroupRequest, Data.Entities.RoleGroup>();
            Mapper.CreateMap<Data.Entities.RoleGroup, UpdateRoleGroupResponse>();

            Mapper.CreateMap<Data.Entities.Type, GetTypeResponse>();
            Mapper.CreateMap<Data.Entities.Type, GetTypesResponse>();
            Mapper.CreateMap<Data.Entities.Type, GetTypesResponse.Type>();
            Mapper.CreateMap<CreateTypeRequest, Data.Entities.Type>();
            Mapper.CreateMap<UpdateTypeRequest, Data.Entities.Type>();
            Mapper.CreateMap<Data.Entities.Type, UpdateTypeResponse>();
            Mapper.CreateMap<KpiAchievement, GetPmsConfigDetailsResponse.KpiAchievment>()
                .ForMember(k => k.Period, o => o.MapFrom(k => k.Periode.ToString("MMM")))
                .ForMember(k => k.Type, o => o.MapFrom(k => k.PeriodeType.ToString()));

            Mapper.CreateMap<Data.Entities.Pillar, GetPillarsResponse>();
            Mapper.CreateMap<Data.Entities.Pillar, GetPillarResponse>();
            Mapper.CreateMap<Data.Entities.Pillar, GetPillarsResponse.Pillar>();
            Mapper.CreateMap<CreatePillarRequest, Data.Entities.Pillar>();
            Mapper.CreateMap<UpdatePillarRequest, Data.Entities.Pillar>();

            base.Configure();
        }
    }
}

