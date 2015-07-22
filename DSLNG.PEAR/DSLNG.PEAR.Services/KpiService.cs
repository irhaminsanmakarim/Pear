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
            if (request.Id != 0)
            {
                query.Where(x => x.Id == request.Id);
            }
            return query.FirstOrDefault().MapTo<GetKpiResponse>();
        }


        public GetKpiToSeriesResponse GetKpiToSeries(GetKpiToSeriesRequest request)
        {
            if (request.MeasurementId != 0)
            {
                return new GetKpiToSeriesResponse
                {
                    KpiList = DataContext.Kpis.Where(x => x.Name.Contains(request.Term) && x.Measurement.Id == request.MeasurementId).Take(20).ToList()
                    .MapTo<GetKpiToSeriesResponse.Kpi>()
                };
            }
            else
            {
                return new GetKpiToSeriesResponse
               {
                   KpiList = DataContext.Kpis.Where(x => x.Name.Contains(request.Term)).Take(20).ToList()
                   .MapTo<GetKpiToSeriesResponse.Kpi>()
               };
            }
        }

        public GetKpiResponse GetKpi(GetKpiRequest request)
        {
            try
            {
                var kpi = DataContext.Kpis
                    .Include(x => x.Pillar)
                    .Include(x => x.Level)
                    .Include(x => x.RoleGroup)
                    .Include(x => x.Group)
                    .Include(x => x.Type)
                    .Include(x => x.Measurement)
                    .Include(x => x.Method)
                    .Include(x => x.RelationModels)
                    .Include("RelationModels.Kpi").First(x => x.Id == request.Id);
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
                kpis = DataContext.Kpis.Include(x => x.Pillar).OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take);
            }
            else
            {
                kpis = DataContext.Kpis.Include(x => x.Pillar);
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
                        if (item.KpiId != 0)
                        {
                            var kpiRelation = DataContext.Kpis.FirstOrDefault(x => x.Id == item.KpiId);
                            relation.Add(new DSLNG.PEAR.Data.Entities.KpiRelationModel
                            {
                                Kpi = kpiRelation,
                                Method = item.Method
                            });
                        }
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
                var updateKpi = request.MapTo<Kpi>();
                var relation = new List<DSLNG.PEAR.Data.Entities.KpiRelationModel>();
                foreach (var kpiRelation in request.RelationModels)
                {
                    relation.Add(new Data.Entities.KpiRelationModel
                    {
                        Id = kpiRelation.Id,
                        Kpi = DataContext.Kpis.SingleOrDefault(x => x.Id == kpiRelation.KpiId),
                        KpiParent = DataContext.Kpis.SingleOrDefault(x => x.Id == request.Id),
                        Method = kpiRelation.Method
                    });
                }
                updateKpi.RelationModels = relation;
                if (request.PillarId.HasValue)
                {
                    updateKpi.Pillar = DataContext.Pillars.FirstOrDefault(x => x.Id == request.PillarId);
                }
                if (request.GroupId.HasValue)
                {
                    updateKpi.Group = DataContext.Groups.FirstOrDefault(x => x.Id == request.GroupId);
                }
                if (request.RoleGroupId.HasValue)
                {
                    updateKpi.RoleGroup = DataContext.RoleGroups.FirstOrDefault(x => x.Id == request.RoleGroupId.Value);
                }
                //if (request.MeasurementId.HasValue)
                //{
                //    updateKpi.Measurement = DataContext.Measurements.FirstOrDefault(x => x.Id == request.MeasurementId);
                //}

                updateKpi.Measurement = DataContext.Measurements.Single(x => x.Id == request.MeasurementId);
                updateKpi.Level = DataContext.Levels.FirstOrDefault(x => x.Id == request.LevelId);
                updateKpi.Type = DataContext.Types.FirstOrDefault(x => x.Id == request.TypeId);
                updateKpi.Method = DataContext.Methods.FirstOrDefault(x => x.Id == request.MethodId);

                var existedkpi = DataContext.Kpis
                    .Where(x => x.Id == request.Id)
                    .Include(x => x.RelationModels)
                    .Include(x => x.Pillar)
                    .Include(x => x.Level)
                    .Include(x => x.RoleGroup)
                    .Include(x => x.Group)
                    .Include(x => x.Type)
                    .Include(x => x.Measurement)
                    .Include(x => x.Method)
                    .Single();

                DataContext.Entry(existedkpi).CurrentValues.SetValues(updateKpi);

                if (updateKpi.Group != null)
                {
                    DataContext.Groups.Attach(updateKpi.Group);
                    existedkpi.Group = updateKpi.Group;
                }

                if (updateKpi.RoleGroup != null)
                {
                    DataContext.RoleGroups.Attach(updateKpi.RoleGroup);
                    existedkpi.RoleGroup = updateKpi.RoleGroup;
                }

                if (updateKpi.Pillar != null)
                {
                    DataContext.Pillars.Attach(updateKpi.Pillar);
                    existedkpi.Pillar = updateKpi.Pillar;
                }
                DataContext.Measurements.Attach(updateKpi.Measurement);
                existedkpi.Measurement = updateKpi.Measurement;

                DataContext.Levels.Attach(updateKpi.Level);
                existedkpi.Level = updateKpi.Level;

                DataContext.Types.Attach(updateKpi.Type);
                existedkpi.Type = updateKpi.Type;

                DataContext.Methods.Attach(updateKpi.Method);
                existedkpi.Method = updateKpi.Method;

                foreach (var relationModel in updateKpi.RelationModels)
                {
                    var existedrelationModel = existedkpi.RelationModels.SingleOrDefault(x => x.Id == relationModel.Id && x.Id != 0);
                    if (existedrelationModel != null)
                    {
                        var relationModelEntry = DataContext.Entry(existedrelationModel);
                        relationModelEntry.CurrentValues.SetValues(relationModel);
                        DataContext.Kpis.Attach(relationModel.Kpi);
                        existedrelationModel.Kpi = relationModel.Kpi;
                    }
                    else
                    {
                        relationModel.Id = 0;
                        existedkpi.RelationModels.Add(relationModel);
                    }
                }

                foreach (var item in existedkpi.RelationModels.Where(x => x.Id != 0).ToList())
                {
                    if (updateKpi.RelationModels.All(x => x.Id != item.Id))
                    {
                        DataContext.KpiRelationModels.Remove(item);
                    }
                }

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
