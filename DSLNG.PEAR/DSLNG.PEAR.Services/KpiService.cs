using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Kpi;
using DSLNG.PEAR.Services.Responses.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Data.Entities;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace DSLNG.PEAR.Services
{
    public class KpiService : BaseService, IKpiService
    {
        public KpiService(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public GetKpiResponse GetBy(GetKpiRequest request)
        {
            var query = DataContext.Kpis;
            if (request.Id != 0) {
                query.Where(x => x.Id == request.Id);
            }
            return query.FirstOrDefault().MapTo<GetKpiResponse>();
        }


        public GetKpiToSeriesResponse GetKpiToSeries()
        {
            return new GetKpiToSeriesResponse
            {
                KpiList = DataContext.Kpis.ToList()
                .MapTo<GetKpiToSeriesResponse.Kpi>()
            };
        }

        public GetKpiResponse GetKpi(GetKpiRequest request)
        {
            try
            {
                var kpi = DataContext.Kpis.First(x => x.Id == request.Id);
                var response = kpi.MapTo<GetKpiResponse>(); 

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetKpiResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }
        public GetKpisResponse GetKpis(GetKpisRequest request)
        {
            IQueryable<Kpi> kpis;
            //var kpis = new Queryable<Kpi>();
            if (request.Take != 0)
            {
                kpis = DataContext.Kpis.OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take);
            }
            else
            {
                kpis = DataContext.Kpis;
            }

            if (request.PillarId > 0)
            {
                kpis = kpis.Include(x => x.Pillar).Where(x => x.Pillar.Id == request.PillarId);
            }

            var response = new GetKpisResponse();
            response.Kpis = kpis.ToList().MapTo<GetKpisResponse.Kpi>();

            return response;
        }

        public CreateKpiResponse Create(CreateKpiRequest request)
        {
            var response = new CreateKpiResponse();
            try
            {
                var kpi = request.MapTo<Kpi>();
                if (request.PillarId.HasValue)
                {
                    kpi.Pillar = DataContext.Pillars.FirstOrDefault(x => x.Id == request.PillarId);
                }
                if (request.GroupId.HasValue)
                {
                    kpi.Group = DataContext.Groups.FirstOrDefault(x => x.Id == request.GroupId);                
                }
                if (request.RoleGroupId.HasValue)
                {
                    kpi.RoleGroup = DataContext.RoleGroups.FirstOrDefault(x => x.Id == request.RoleGroupId.Value);                    
                }
                if (request.MeasurementId.HasValue)
                {
                    kpi.Measurement = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementId);                    
                }
                kpi.Level = DataContext.Levels.FirstOrDefault(x => x.Id == request.LevelId);
                kpi.Type = DataContext.Types.FirstOrDefault(x => x.Id == request.TypeId);
                kpi.Method = DataContext.Methods.FirstOrDefault(x => x.Id == request.MethodId);
                if (request.RelationModels.Count > 0)
                {
                    var relation = new List<DSLNG.PEAR.Data.Entities.KpiRelationModel>();
                    foreach (var item in request.RelationModels)
                    {
                        var kpiRelation = DataContext.Kpis.FirstOrDefault(x=>x.Id == item.Id);
                        relation.Add(new DSLNG.PEAR.Data.Entities.KpiRelationModel
                        {
                            Kpi = kpiRelation,
                            Method = item.Method
                        });
                    }
                    kpi.RelationModels = relation;
                }
                
                DataContext.Kpis.Add(kpi);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdateKpiResponse Update(UpdateKpiRequest request)
        {
            var response = new UpdateKpiResponse();
            try
            {
                var kpi = request.MapTo<Kpi>();
                DataContext.Kpis.Attach(kpi);
                DataContext.Entry(kpi).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeleteKpiResponse Delete(int id)
        {
            var response = new DeleteKpiResponse();
            try
            {
                var kpi = new Kpi { Id = id };
                DataContext.Kpis.Attach(kpi);
                DataContext.Entry(kpi).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "KPI item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
