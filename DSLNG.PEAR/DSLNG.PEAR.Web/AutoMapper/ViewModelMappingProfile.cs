using AutoMapper;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.Kpi;
using DSLNG.PEAR.Web.ViewModels.Kpi;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Web.ViewModels.Level;
using DSLNG.PEAR.Web.ViewModels.Measurement;
using DSLNG.PEAR.Web.ViewModels.Menu;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Web.ViewModels.User;

using DSLNG.PEAR.Services.Requests.RoleGroup;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using DSLNG.PEAR.Web.ViewModels.RoleGroup;


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
            Mapper.CreateMap<GetMenuResponse, MenusViewModel>();
            
            Mapper.CreateMap<CreateMeasurementViewModel, CreateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementResponse, UpdateMeasurementViewModel>();
            Mapper.CreateMap<UpdateMeasurementViewModel, UpdateMeasurementRequest>();
            Mapper.CreateMap<GetMeasurementsResponse.Measurement, MeasurementViewModel>();
            Mapper.CreateMap<LevelViewModel, UpdateLevelRequest>();
            Mapper.CreateMap<LevelViewModel, CreateLevelRequest>();

            Mapper.CreateMap<GetUsersResponse.User, UserViewModel>();
            Mapper.CreateMap<GetRoleGroupsResponse.RoleGroup, RoleGroupViewModel>();
            
            base.Configure();
        }
    }
}