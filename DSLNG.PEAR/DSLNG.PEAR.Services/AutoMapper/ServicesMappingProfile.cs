using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Requests.Measurement;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Services.Requests.Menu;
using DSLNG.PEAR.Services.Responses.PmsSummary;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.Group;
using DSLNG.PEAR.Services.Requests.Group;
using DSLNG.PEAR.Services.Responses.Kpi;
using DSLNG.PEAR.Services.Responses.Measurement;
using DSLNG.PEAR.Services.Responses.Conversion;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.RoleGroup;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using DSLNG.PEAR.Services.Responses.Type;
using DSLNG.PEAR.Services.Requests.Type;
using DSLNG.PEAR.Services.Responses.Pillar;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Requests.Method;
using DSLNG.PEAR.Services.Responses.Method;
using DSLNG.PEAR.Services.Requests.Periode;
using DSLNG.PEAR.Services.Responses.Periode;
using DSLNG.PEAR.Services.Requests.Artifact;
using DSLNG.PEAR.Services.Responses.Artifact;
using DSLNG.PEAR.Services.Requests.KpiTarget;
using DSLNG.PEAR.Services.Requests.Conversion;
using DSLNG.PEAR.Services.Responses.KpiTarget;

namespace DSLNG.PEAR.Services.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        protected override void Configure()
        {
            ConfigurePmsSummary();
            ConfigureKpi();
            ConfigureKpiTarget();

            Mapper.CreateMap<User, GetUsersResponse.User>();
                //.ForMember(x => x.RoleName, o => o.MapFrom(m => m.Role.Name));
            Mapper.CreateMap<CreateUserRequest, User>();
            Mapper.CreateMap<UpdateUserRequest, User>();
            Mapper.CreateMap<GetUserRequest, User>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetUserResponse.RoleGroup>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetUsersResponse.RoleGroup>();
            Mapper.CreateMap<Data.Entities.User, GetUserResponse>();
            /*Level*/
            Mapper.CreateMap<Data.Entities.Level, GetLevelsResponse.Level>();
            Mapper.CreateMap<Data.Entities.Level, GetLevelResponse>();
            Mapper.CreateMap<CreateLevelRequest, Data.Entities.Level>();
            Mapper.CreateMap<UpdateLevelRequest, Data.Entities.Level>();
            Mapper.CreateMap<Data.Entities.Level, UpdateLevelResponse>();


            Mapper.CreateMap<Data.Entities.Menu, GetMenusResponse.Menu>()
                //.ForMember(m => m.RoleGroups, o => o.MapFrom(m => m.RoleGroups.MapTo<GetMenuResponse.RoleGroup>()))
                .ForMember(m => m.Menus, o => o.MapFrom(m => m.Menus.MapTo<GetMenusResponse.Menu>()));
            Mapper.CreateMap<CreateMenuRequest, Data.Entities.Menu>();
            Mapper.CreateMap<Data.Entities.Menu, GetMenusResponse.Menu>();
            Mapper.CreateMap<Data.Entities.Menu, GetMenuResponse>()
                .ForMember(x => x.RoleGroups, o => o.MapFrom(k => k.RoleGroups));

            Mapper.CreateMap<UpdateMenuRequest, Data.Entities.Menu>();
            //Mapper.CreateMap<Data.Entities.RoleGroup, GetMenusResponse.RoleGroup>();
            //Mapper.CreateMap<Data.Entities.Level, GetMenusResponse.Level>();
            Mapper.CreateMap<Data.Entities.Level, Responses.Menu.Level>();
            Mapper.CreateMap<Data.Entities.RoleGroup, Responses.Menu.RoleGroup>();
            Mapper.CreateMap<Data.Entities.Group, GetGroupResponse>();
            Mapper.CreateMap<Data.Entities.Group, GetGroupsResponse.Group>();
            Mapper.CreateMap<CreateGroupRequest, Data.Entities.Group>();
            Mapper.CreateMap<UpdateGroupRequest, Data.Entities.Group>();
            //Mapper.CreateMap<Activity, GetGroupResponse.Activity>();


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
            Mapper.CreateMap<CreateKpiRequest, Data.Entities.Type>();
            Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.KpiRelationModel, DSLNG.PEAR.Data.Entities.KpiRelationModel>();
            //Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.Level, Data.Entities.Level>();
            Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.RoleGroup, Data.Entities.RoleGroup>();
            //Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.Type, Data.Entities.Type>();
            //Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.Group, Data.Entities.Group>();
            //Mapper.CreateMap<DSLNG.PEAR.Services.Requests.Kpi.Measurement, Data.Entities.Measurement>();
            Mapper.CreateMap<Data.Entities.Level, DSLNG.PEAR.Services.Requests.Kpi.Level>();
            //Mapper.CreateMap<Data.Entities.RoleGroup, DSLNG.PEAR.Services.Requests.Kpi.RoleGroup>();
            Mapper.CreateMap<Data.Entities.Group, DSLNG.PEAR.Services.Requests.Kpi.Group>();
            Mapper.CreateMap<Data.Entities.Measurement, DSLNG.PEAR.Services.Requests.Kpi.Measurement>();
            Mapper.CreateMap<Data.Entities.Level, GetKpisResponse.Level>();
            Mapper.CreateMap<Data.Entities.RoleGroup, GetKpisResponse.RoleGroup>();
            Mapper.CreateMap<Data.Entities.Type, GetKpisResponse.Type>();
            Mapper.CreateMap<Data.Entities.Pillar, GetKpisResponse.Pillar>();
            Mapper.CreateMap<Kpi, GetKpiResponse>();
            Mapper.CreateMap<UpdateKpiRequest, Kpi>();

            Mapper.CreateMap<Data.Entities.Measurement, GetMeasurementsResponse>();
            Mapper.CreateMap<Data.Entities.Method, GetMethodResponse>();

            Mapper.CreateMap<Data.Entities.Conversion, GetConversionResponse>();

            Mapper.CreateMap<Data.Entities.RoleGroup, GetRoleGroupsResponse.RoleGroup>()
                .ForMember(x => x.LevelName, o => o.MapFrom(k=>k.Level.Name));
            Mapper.CreateMap<Data.Entities.Level, Responses.RoleGroup.Level>();
            //Mapper.CreateMap<Data.Entities.RoleGroup, GetRoleGroupsResponse>();
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
            Mapper.CreateMap<KpiAchievement, GetPmsDetailsResponse.KpiAchievment>()
                .ForMember(k => k.Period, o => o.MapFrom(k => k.Periode.ToString("MMM")))
                .ForMember(k => k.Type, o => o.MapFrom(k => k.PeriodeType.ToString()));

            Mapper.CreateMap<Data.Entities.Pillar, GetPillarsResponse>();
            Mapper.CreateMap<Data.Entities.Pillar, GetPillarResponse>();
            Mapper.CreateMap<Data.Entities.Pillar, GetPillarsResponse.Pillar>();
            
            Mapper.CreateMap<CreatePillarRequest, Data.Entities.Pillar>();
            Mapper.CreateMap<UpdatePillarRequest, Data.Entities.Pillar>();

            Mapper.CreateMap<Data.Entities.Method, GetMethodResponse>();
            Mapper.CreateMap<CreateMethodRequest, Data.Entities.Method>();
            Mapper.CreateMap<Data.Entities.Method, GetMethodsResponse.Method>();
            Mapper.CreateMap<UpdateMethodRequest, Data.Entities.Method>();
            
            Mapper.CreateMap<Data.Entities.Method, GetMethodsResponse.Method>();
            Mapper.CreateMap<Data.Entities.Group, GetGroupResponse>();
            
            Mapper.CreateMap<Data.Entities.Method, Responses.Kpi.Method>();
            Mapper.CreateMap<Data.Entities.Method, GetMethodResponse>();
            Mapper.CreateMap<Data.Entities.Level, Data.Entities.Kpi>();
            Mapper.CreateMap<Data.Entities.Periode, GetPeriodesResponse.Periode>();
            Mapper.CreateMap<Data.Entities.Periode, GetPeriodeResponse>();
            Mapper.CreateMap<CreatePeriodeRequest, Data.Entities.Periode>();
            Mapper.CreateMap<UpdatePeriodeRequest, Data.Entities.Periode>();

            Mapper.CreateMap<CreateArtifactRequest, Data.Entities.Artifact>()
                .ForMember(x => x.Series, o => o.Ignore())
                .ForMember(x => x.Plots, o => o.Ignore());
            Mapper.CreateMap<CreateArtifactRequest.SeriesRequest, Data.Entities.ArtifactSerie>();
            Mapper.CreateMap<CreateArtifactRequest.PlotRequest, Data.Entities.ArtifactPlot>();
            Mapper.CreateMap<CreateArtifactRequest.StackRequest, Data.Entities.ArtifactStack>();

            Mapper.CreateMap<Artifact, GetArtifactsResponse.Artifact>();
            Mapper.CreateMap<Artifact, GetArtifactResponse>()
                .ForMember(x => x.PlotBands, o => o.MapFrom(s => s.Plots.MapTo<GetArtifactResponse.PlotResponse>()))
                .ForMember(x => x.Series, o => o.MapFrom(s => s.Series.MapTo<GetArtifactResponse.SeriesResponse>()))
                .ForMember(x => x.Measurement, o => o.MapFrom(s => s.Measurement.Name));
            Mapper.CreateMap<ArtifactPlot, GetArtifactResponse.PlotResponse>();
            Mapper.CreateMap<ArtifactSerie, GetArtifactResponse.SeriesResponse>()
                .ForMember(x => x.Stacks, o => o.MapFrom(s => s.Stacks.MapTo<GetArtifactResponse.StackResponse>()))
                .ForMember(x => x.KpiId, o => o.MapFrom(s => s.Kpi.Id));
            Mapper.CreateMap<ArtifactStack, GetArtifactResponse.StackResponse>()
                .ForMember(x => x.KpiId, o => o.MapFrom(s => s.Kpi.Id));
            Mapper.CreateMap<CreateConversionRequest, Data.Entities.Conversion>();
            Mapper.CreateMap<Data.Entities.Conversion, GetConversionsResponse.Conversion>()
                .ForMember(f => f.FromName, o => o.MapFrom(k => k.From.Name))
                .ForMember(f => f.ToName, o => o.MapFrom(k => k.To.Name));
            Mapper.CreateMap<Data.Entities.Conversion, GetConversionResponse>();
            Mapper.CreateMap<Data.Entities.Measurement, Responses.Conversion.Measurement>();
            Mapper.CreateMap<UpdateConversionRequest, Data.Entities.Conversion>();
            Mapper.CreateMap<KpiTarget, GetKpiTargetsResponse.KpiTarget>()
               .ForMember(k => k.KpiName, o => o.MapFrom(k => k.Kpi.Name))
               .ForMember(k => k.PeriodeType, o => o.MapFrom(k => k.PeriodeType.ToString()));

            base.Configure();
        }

        private void ConfigurePmsSummary()
        {
            Mapper.CreateMap<PmsSummary, GetPmsSummaryListResponse.PmsSummary>();
            Mapper.CreateMap<Kpi, GetPmsSummaryConfigurationResponse.Kpi>()
                  .ForMember(x => x.Measurement, y => y.MapFrom(z => z.Measurement.Name));
            Mapper.CreateMap<Data.Entities.Pillar, GetPmsSummaryConfigurationResponse.Pillar>();
            Mapper.CreateMap<PmsConfig, GetPmsSummaryConfigurationResponse.PmsConfig>()
                .ForMember(x => x.PillarId, y => y.MapFrom(z => z.Pillar.Id))
                .ForMember(x => x.PillarName, y => y.MapFrom(z => z.Pillar.Name))
                .ForMember(x => x.PmsConfigDetailsList, y => y.MapFrom(z => z.PmsConfigDetailsList));
            Mapper.CreateMap<ScoreIndicator, GetPmsSummaryConfigurationResponse.ScoreIndicator>();
            Mapper.CreateMap<PmsConfigDetails, GetPmsSummaryConfigurationResponse.PmsConfigDetails>();
            Mapper.CreateMap<ScoreIndicator, GetScoreIndicatorsResponse.ScoreIndicator>();
            Mapper.CreateMap<ScoreIndicator, GetPmsDetailsResponse.ScoreIndicator>();

            Mapper.CreateMap<PmsConfigDetails, GetPmsConfigDetailsResponse>()
                  .ForMember(x => x.KpiId, y => y.MapFrom(z => z.Kpi.Id))
                  .ForMember(x => x.PillarId, y => y.MapFrom(z => z.Kpi.Pillar.Id));

            Mapper.CreateMap<Kpi, GetPmsConfigDetailsResponse.Kpi>()
                  .ForMember(x => x.PillarId, y => y.MapFrom(z => z.Pillar.Id));
            Mapper.CreateMap<Data.Entities.Pillar, GetPmsConfigDetailsResponse.Pillar>();
            Mapper.CreateMap<ScoreIndicator, GetPmsConfigDetailsResponse.ScoreIndicator>();
            

            /*Mapper.CreateMap<Data.Entities.PmsConfigDetails, GetPmsSummaryConfigurationResponse.PmsConfigDetails>()
                .ForMember(x => x.);*/

            Mapper.CreateMap<CreateKpiTargetRequest.KpiTarget, KpiTarget>();
            Mapper.CreateMap<Kpi, GetKpisByPillarIdResponse.Kpi>();
        }
        
        private void ConfigureKpi()
        {
            Mapper.CreateMap<Data.Entities.Group, GetKpisResponse.Group>();
            Mapper.CreateMap<Data.Entities.Measurement, GetKpisResponse.Measurement>();
            Mapper.CreateMap<Data.Entities.Pillar, GetKpisResponse.Pillar>();
        }

        private void ConfigureKpiTarget()
        {
            Mapper.CreateMap<Data.Entities.Kpi, GetPmsConfigsResponse.Kpi>();
            Mapper.CreateMap<Data.Entities.Measurement, GetPmsConfigsResponse.Measurement>();
            Mapper.CreateMap<Data.Entities.PmsConfigDetails, GetPmsConfigsResponse.PmsConfigDetails>();
            Mapper.CreateMap<Data.Entities.Pillar, GetPmsConfigsResponse.Pillar>();
            Mapper.CreateMap<Data.Entities.PmsSummary, GetPmsConfigsResponse.PmsSummary>();
            Mapper.CreateMap<Data.Entities.PmsConfig, GetPmsConfigsResponse.PmsConfig>();
        }
    }
}

