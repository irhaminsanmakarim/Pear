using AutoMapper;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.Kpi;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.Kpi;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Web.ViewModels.Level;
using DSLNG.PEAR.Web.ViewModels.Measurement;
using DSLNG.PEAR.Web.ViewModels.Menu;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Web.ViewModels.PmsSummary;
using DSLNG.PEAR.Web.ViewModels.User;
using DSLNG.PEAR.Web.ViewModels.RoleGroup;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using DSLNG.PEAR.Web.ViewModels.Type;
using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;


namespace DSLNG.PEAR.Web.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GetLevelResponse, LevelViewModel>();
            Mapper.CreateMap<GetLevelsResponse, LevelsViewModel>();
            Mapper.CreateMap<GetLevelsResponse.Level, LevelViewModel>();

            Mapper.CreateMap<GetKpiToSeriesResponse, KpiToSeriesViewModel>();

            Mapper.CreateMap<GetMenusResponse.Menu, MenusViewModel>();
            Mapper.CreateMap<CreateMenuViewModel, CreateMenuRequest>();
            Mapper.CreateMap<GetMenuResponse, UpdateMenuViewModel>();
            Mapper.CreateMap<UpdateMenuViewModel, UpdateMenuRequest>();
            
            Mapper.CreateMap<CreateMeasurementViewModel, CreateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementResponse, UpdateMeasurementViewModel>();
            Mapper.CreateMap<UpdateMeasurementViewModel, UpdateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementsResponse.Measurement, MeasurementViewModel>();

            Mapper.CreateMap<LevelViewModel, UpdateLevelRequest>();
            Mapper.CreateMap<LevelViewModel, CreateLevelRequest>();

            Mapper.CreateMap<GetUsersResponse.User, UserViewModel>();
            Mapper.CreateMap<CreateUserViewModel, CreateUserRequest>();
            Mapper.CreateMap<GetUserResponse, UpdateUserViewModel>();
            Mapper.CreateMap<UpdateUserViewModel, UpdateUserRequest>();

            Mapper.CreateMap<GetRoleGroupsResponse.RoleGroup, RoleGroupViewModel>();
            Mapper.CreateMap<CreateRoleGroupViewModel, CreateRoleGroupRequest>();
            Mapper.CreateMap<GetRoleGroupResponse,UpdateRoleGroupViewModel>();
            Mapper.CreateMap<UpdateRoleGroupViewModel, UpdateRoleGroupRequest>();


            Mapper.CreateMap<GetTypeResponse, UpdateTypeViewModel>();
            Mapper.CreateMap<GetTypesResponse.Type, TypeViewModel>();
            Mapper.CreateMap<CreateTypeViewModel, CreateTypeRequest>();
            Mapper.CreateMap<UpdateTypeViewModel, UpdateTypeRequest>();

            Mapper.CreateMap<GetPmsSummaryResponse.KpiData, PmsSummaryViewModel>();
            
            
            base.Configure();
        }
    }
}