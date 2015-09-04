﻿using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Enums;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.KpiTarget;
using DSLNG.PEAR.Services.Responses.KpiTarget;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity;
using DSLNG.PEAR.Services.Responses;

namespace DSLNG.PEAR.Services
{
    public class KpiTargetService : BaseService, IKpiTargetService
    {
        public KpiTargetService(IDataContext dataContext)
            : base(dataContext)
        {
        }
        public CreateKpiTargetsResponse Creates(CreateKpiTargetsRequest request)
        {
            var response = new CreateKpiTargetsResponse();
            try
            {
                if (request.KpiTargets.Count > 0)
                {
                    foreach (var kpiTarget in request.KpiTargets)
                    {
                        var data = kpiTarget.MapTo<KpiTarget>();
                        data.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == kpiTarget.KpiId);
                        DataContext.KpiTargets.Add(data);
                        DataContext.SaveChanges();
                    }
                    response.IsSuccess = true;
                    response.Message = "KPI Target has been added successfully";
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            return response;
        }


        public GetPmsConfigsResponse GetPmsConfigs(GetPmsConfigsRequest request)
        {
            var pmsSummary = DataContext.PmsSummaries
                                            .Include("PmsConfigs.Pillar")
                                            .Include("PmsConfigs.PmsConfigDetailsList.Kpi.Measurement")
                                            .FirstOrDefault(x => x.Id == request.Id);
            var response = new GetPmsConfigsResponse();
            var pmsConfigsList = new List<PmsConfig>();
            if (pmsSummary != null)
            {
                var pmsConfigs = pmsSummary.PmsConfigs.ToList();
                if (pmsConfigs.Count > 0)
                {
                    response.PmsConfigs = pmsConfigs.MapTo<GetPmsConfigsResponse.PmsConfig>();
                }
            }

            return response;
        }


        public GetKpiTargetsResponse GetKpiTargets(GetKpiTargetsRequest request)
        {
            var kpis = new List<KpiTarget>();
            if (request.Take != 0)
            {
                kpis = DataContext.KpiTargets.Include(x => x.Kpi).OrderBy(x => x.Id).Skip(request.Skip).Take(request.Take).ToList();
            }
            else
            {
                kpis = DataContext.KpiTargets.Include(x => x.Kpi).ToList();
            }
            var response = new GetKpiTargetsResponse();
            response.KpiTargets = kpis.MapTo<GetKpiTargetsResponse.KpiTarget>();

            return response;
        }

        public GetKpiTargetResponse GetKpiTarget(GetKpiTargetRequest request)
        {
            var response = new GetKpiTargetResponse();
            try
            {
                var pmsSummary = DataContext.PmsSummaries.Single(x => x.Id == request.PmsSummaryId);

                var pillarsAndKpis = DataContext.PmsConfigDetails
                        .Include(x => x.Kpi)
                        .Include(x => x.Kpi.KpiTargets)
                        .Include(x => x.Kpi.Measurement)
                        .Include(x => x.PmsConfig)
                        .Include(x => x.PmsConfig.PmsSummary)
                        .Include(x => x.PmsConfig.Pillar)
                        .Where(x => x.PmsConfig.PmsSummary.Id == request.PmsSummaryId)
                        .ToList()
                        .GroupBy(x => x.PmsConfig.Pillar)
                        .ToDictionary(x => x.Key);


                foreach (var item in pillarsAndKpis)
                {
                    var pillar = new GetKpiTargetResponse.Pillar() { Id = item.Key.Id, Name = item.Key.Name };

                    foreach (var val in item.Value)
                    {
                        var targets = new List<GetKpiTargetResponse.KpiTarget>();
                        switch (request.PeriodeType)
                        {
                            case PeriodeType.Monthly:
                                for (int i = 1; i <= 12; i++)
                                {
                                    var kpiTargetsMonthly = val.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Monthly
                                                    && x.Periode.Month == i && x.Periode.Year == pmsSummary.Year);
                                    var kpiTargetMonthly = new GetKpiTargetResponse.KpiTarget();
                                    if (kpiTargetsMonthly == null)
                                    {
                                        kpiTargetMonthly.Id = 0;
                                        kpiTargetMonthly.Periode = new DateTime(pmsSummary.Year, i, 1);
                                        kpiTargetMonthly.Value = null;
                                        kpiTargetMonthly.Remark = null;
                                    }
                                    else
                                    {
                                        kpiTargetMonthly.Id = kpiTargetsMonthly.Id;
                                        kpiTargetMonthly.Periode = kpiTargetsMonthly.Periode;
                                        kpiTargetMonthly.Value = kpiTargetsMonthly.Value;
                                        kpiTargetMonthly.Remark = kpiTargetsMonthly.Remark;
                                    }

                                    targets.Add(kpiTargetMonthly);
                                }
                                break;
                            case PeriodeType.Yearly:
                                var kpiTargetsYearly =
                                    val.Kpi.KpiTargets.FirstOrDefault(x => x.PeriodeType == PeriodeType.Yearly
                                                                           && x.Periode.Year == pmsSummary.Year);
                                var kpiTargetYearly = new GetKpiTargetResponse.KpiTarget();
                                if (kpiTargetsYearly == null)
                                {
                                    kpiTargetYearly.Id = 0;
                                    kpiTargetYearly.Periode = new DateTime(pmsSummary.Year, 1, 1);
                                    kpiTargetYearly.Value = null;
                                    kpiTargetYearly.Remark = null;
                                }
                                else
                                {
                                    kpiTargetYearly.Id = kpiTargetsYearly.Id;
                                    kpiTargetYearly.Periode = kpiTargetsYearly.Periode;
                                    kpiTargetYearly.Value = kpiTargetsYearly.Value;
                                    kpiTargetYearly.Remark = kpiTargetsYearly.Remark;
                                }
                                targets.Add(kpiTargetYearly);

                                break;
                        }

                        var kpi = new GetKpiTargetResponse.Kpi
                            {
                                Id = val.Kpi.Id,
                                Measurement = val.Kpi.Measurement.Name,
                                Name = val.Kpi.Name,
                                KpiTargets = targets
                            };

                        pillar.Kpis.Add(kpi);
                    }

                    response.Pillars.Add(pillar);
                }
                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        public UpdateKpiTargetResponse UpdateKpiTarget(UpdateKpiTargetRequest request)
        {
            PeriodeType periodeType = (PeriodeType)Enum.Parse(typeof(PeriodeType), request.PeriodeType);
            var response = new UpdateKpiTargetResponse();
            response.PeriodeType = periodeType;
            try
            {
                foreach (var pillar in request.Pillars)
                {
                    foreach (var kpi in pillar.Kpis)
                    {
                        foreach (var kpiTarget in kpi.KpiTargets)
                        {
                            if (kpiTarget.Id == 0)
                            {
                                var kpiTargetNew = new KpiTarget();
                                kpiTargetNew.Value = kpiTarget.Value;
                                kpiTargetNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiTargetNew.PeriodeType = periodeType;
                                kpiTargetNew.Periode = kpiTarget.Periode;
                                kpiTargetNew.IsActive = true;
                                kpiTargetNew.Remark = kpiTarget.Remark;
                                kpiTargetNew.CreatedDate = DateTime.Now;
                                kpiTargetNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiTargets.Add(kpiTargetNew);
                            }
                            else
                            {
                                var kpiTargetNew = new KpiTarget();
                                kpiTargetNew.Id = kpiTarget.Id;
                                kpiTargetNew.Value = kpiTarget.Value;
                                kpiTargetNew.Kpi = DataContext.Kpis.Single(x => x.Id == kpi.Id);
                                kpiTargetNew.PeriodeType = periodeType;
                                kpiTargetNew.Periode = kpiTarget.Periode;
                                kpiTargetNew.IsActive = true;
                                kpiTargetNew.Remark = kpiTarget.Remark;
                                kpiTargetNew.UpdatedDate = DateTime.Now;
                                DataContext.KpiTargets.Attach(kpiTargetNew);
                                DataContext.Entry(kpiTargetNew).State = EntityState.Modified;
                            }
                        }
                    }
                }
                response.IsSuccess = true;
                response.Message = "KPI Target has been updated successfully";
                DataContext.SaveChanges();
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }

        public CreateKpiTargetResponse Create(CreateKpiTargetRequest request)
        {
            var response = new CreateKpiTargetResponse();
            try
            {

                var data = request.MapTo<KpiTarget>();
                data.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == request.KpiId);
                DataContext.KpiTargets.Add(data);
                DataContext.SaveChanges();

                response.IsSuccess = true;
                response.Message = "KPI Target has been added successfully";
                response.Id = data.Id;
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            return response;
        }


        public UpdateKpiTargetItemResponse UpdateKpiTargetItem(UpdateKpiTargetItemRequest request)
        {
            var response = new UpdateKpiTargetItemResponse();
            try
            {
                var kpiTarget = request.MapTo<KpiTarget>();
                DataContext.KpiTargets.Attach(kpiTarget);
                DataContext.Entry(kpiTarget).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.Id = request.Id;
                response.IsSuccess = true;
                response.Message = "KPI Target item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public GetKpiTargetsConfigurationResponse GetKpiTargetsConfiguration(GetKpiTargetsConfigurationRequest request)
        {
            var response = new GetKpiTargetsConfigurationResponse();
            try
            {
                var periodeType = string.IsNullOrEmpty(request.PeriodeType)
                                      ? PeriodeType.Yearly
                                      : (PeriodeType)Enum.Parse(typeof(PeriodeType), request.PeriodeType);

                var kpis = DataContext.Kpis
                                      .Include(x => x.RoleGroup)
                                      .Include(x => x.Measurement).ToList();
                if(request.RoleGroupId>0){
                    kpis = kpis.Where(x => x.RoleGroup.Id == request.RoleGroupId).ToList();
                    var roleGroup = DataContext.RoleGroups.Single(x => x.Id == request.RoleGroupId);
                    response.RoleGroupName = roleGroup.Name;
                    response.RoleGroupId = roleGroup.Id;
                    response.IsSuccess = true;
                }
                                      

                switch (periodeType)
                {
                    case PeriodeType.Yearly:
                        var kpiTargetsYearly = DataContext.KpiTargets
                                        .Include(x => x.Kpi)
                                        .Where(x => x.PeriodeType == periodeType).ToList();
                        foreach (var kpi in kpis)
                        {
                            var kpiDto = kpi.MapTo<GetKpiTargetsConfigurationResponse.Kpi>();
                            foreach (var number in YearlyNumbers)
                            {
                                var achievement = kpiTargetsYearly.SingleOrDefault(x => x.Kpi.Id == kpi.Id && x.Periode.Year == number);
                                if (achievement != null)
                                {
                                    var targetDto =
                                        achievement.MapTo<GetKpiTargetsConfigurationResponse.KpiTarget>();
                                    kpiDto.KpiTargets.Add(targetDto);
                                }
                                else
                                {
                                    var targetDto = new GetKpiTargetsConfigurationResponse.KpiTarget();
                                    targetDto.Periode = new DateTime(number, 1, 1);
                                    kpiDto.KpiTargets.Add(targetDto);
                                }
                            }


                            response.Kpis.Add(kpiDto);
                        }
                        break;

                    case PeriodeType.Monthly:
                        var kpiTargetsMonthly = DataContext.KpiTargets
                                        .Include(x => x.Kpi)
                                        .Where(x => x.PeriodeType == periodeType && x.Periode.Year == request.Year).ToList();
                        foreach (var kpi in kpis)
                        {
                            var kpiDto = kpi.MapTo<GetKpiTargetsConfigurationResponse.Kpi>();
                            var targets = kpiTargetsMonthly.Where(x => x.Kpi.Id == kpi.Id).ToList();

                            for (int i = 1; i <= 12; i++)
                            {
                                var target = targets.FirstOrDefault(x => x.Periode.Month == i);
                                if (target != null)
                                {
                                    var achievementDto =
                                        target.MapTo<GetKpiTargetsConfigurationResponse.KpiTarget>();
                                    kpiDto.KpiTargets.Add(achievementDto);
                                }
                                else
                                {
                                    var achievementDto = new GetKpiTargetsConfigurationResponse.KpiTarget();
                                    achievementDto.Periode = new DateTime(request.Year, i, 1);
                                    kpiDto.KpiTargets.Add(achievementDto);
                                }
                            }
                            response.Kpis.Add(kpiDto);
                        }
                        break;

                    case PeriodeType.Daily:
                        var kpiTargetsDaily = DataContext.KpiTargets
                                        .Include(x => x.Kpi)
                                        .Where(x => x.PeriodeType == periodeType && x.Periode.Year == request.Year
                                        && x.Periode.Month == request.Month).ToList();
                        foreach (var kpi in kpis)
                        {
                            var kpiDto = kpi.MapTo<GetKpiTargetsConfigurationResponse.Kpi>();
                            var targets = kpiTargetsDaily.Where(x => x.Kpi.Id == kpi.Id).ToList();
                            for (int i = 1; i <= DateTime.DaysInMonth(request.Year, request.Month); i++)
                            {
                                var target = targets.FirstOrDefault(x => x.Periode.Day == i);
                                if (target != null)
                                {
                                    var targetDto =
                                        target.MapTo<GetKpiTargetsConfigurationResponse.KpiTarget>();
                                    kpiDto.KpiTargets.Add(targetDto);
                                }
                                else
                                {
                                    var targetDto = new GetKpiTargetsConfigurationResponse.KpiTarget();
                                    targetDto.Periode = new DateTime(request.Year, request.Month, i);
                                    kpiDto.KpiTargets.Add(targetDto);
                                }
                            }
                            response.Kpis.Add(kpiDto);
                        }
                        break;
                }

                
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }

            return response;
        }

        public AllKpiTargetsResponse GetAllKpiTargets()
        {
            var response = new AllKpiTargetsResponse();
            try
            {
                var kpiTargets = DataContext.Kpis
                                            .Include(x => x.Measurement)
                                            .Include(x => x.Type)
                                            .Include(x => x.RoleGroup)
                                            .AsEnumerable()
                                            .OrderBy(x => x.Order)
                                            .GroupBy(x => x.RoleGroup).ToDictionary(x => x.Key);



                foreach (var item in kpiTargets)
                {
                    var kpis = new List<AllKpiTargetsResponse.Kpi>();
                    foreach (var val in item.Value)
                    {
                        kpis.Add(val.MapTo<AllKpiTargetsResponse.Kpi>());
                    }

                    response.RoleGroups.Add(new AllKpiTargetsResponse.RoleGroup
                    {
                        Id = item.Key.Id,
                        Name = item.Key.Name,
                        Kpis = kpis
                    });
                }

                response.IsSuccess = true;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }

            return response;
        }


        public GetKpiTargetItemResponse GetKpiTargetByValue(GetKpiTargetRequestByValue request)
        {
            var response = new GetKpiTargetItemResponse();
            PeriodeType periodeType = (PeriodeType)Enum.Parse(typeof(PeriodeType), request.PeriodeType);
            response.PeriodeType = periodeType;
            try
            {
                var kpiTarget = DataContext.KpiTargets.Include(x => x.Kpi).Single(x => x.Kpi.Id == request.Kpi_Id && x.PeriodeType == periodeType && x.Periode == request.periode);
                response = kpiTarget.MapTo<GetKpiTargetItemResponse>();
                response.IsSuccess = true;
            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = invalidOperationException.Message;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.IsSuccess = false;
                response.Message = argumentNullException.Message;
            }
            return response;
        }

        public UpdateKpiTargetItemResponse SaveKpiTargetItem(SaveKpiTargetRequest request)
        {
            var response = new UpdateKpiTargetItemResponse();
            try
            {
                var kpiTarget = request.MapTo<KpiTarget>();
                
                if (request.Id != 0)
                {
                    var attachedEntity = DataContext.KpiTargets.Find(request.Id);
                    if (attachedEntity != null && DataContext.Entry(attachedEntity).State != EntityState.Detached)
                    {
                        DataContext.Entry(attachedEntity).State = EntityState.Detached;
                    }
                    DataContext.KpiTargets.Attach(kpiTarget);
                    DataContext.Entry(kpiTarget).State = EntityState.Modified;
                    DataContext.SaveChanges();
                }
                else
                {
                    kpiTarget.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == request.KpiId);
                    DataContext.KpiTargets.Add(kpiTarget);
                    DataContext.SaveChanges();
                }
                response.Id = kpiTarget.Id;
                response.IsSuccess = true;
                response.Message = "KPI Target item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }
            return response;
        }


        public BaseResponse BatchUpdateKpiTargetss(BatchUpdateTargetRequest request)
        {
            var response = new BaseResponse();
            try
            {
                int i = 0;
                foreach (var item in request.BatchUpdateKpiTargetItemRequest)
                {
                    var kpiTarget = item.MapTo<KpiTarget>();
                    var exist = DataContext.KpiTargets.FirstOrDefault(x => x.Kpi.Id == item.KpiId && x.PeriodeType == item.PeriodeType && x.Periode == item.Periode && x.Value == item.Value && x.Remark == item.Remark);
                    //skip no change value
                    if (exist != null)
                    {
                        continue;
                    }
                    var attachedEntity = DataContext.KpiTargets.FirstOrDefault(x => x.Kpi.Id == item.KpiId && x.PeriodeType == item.PeriodeType && x.Periode == item.Periode);
                    if (attachedEntity != null)
                    {
                        kpiTarget.Id = attachedEntity.Id;
                    }
                    //jika tidak ada perubahan di skip aja
                    //if (existing.Value.Equals(item.Value) && existing.Periode.Equals(item.Periode) && existing.Kpi.Id.Equals(item.KpiId) && existing.PeriodeType.Equals(item.PeriodeType)) {
                    //    break;
                    //}
                    if (kpiTarget.Id != 0)
                    {
                        //var attachedEntity = DataContext.KpiAchievements.Find(item.Id);
                        if (attachedEntity != null && DataContext.Entry(attachedEntity).State != EntityState.Detached)
                        {
                            DataContext.Entry(attachedEntity).State = EntityState.Detached;
                        }
                        DataContext.KpiTargets.Attach(kpiTarget);
                        DataContext.Entry(kpiTarget).State = EntityState.Modified;
                    }
                    else
                    {
                        kpiTarget.Kpi = DataContext.Kpis.FirstOrDefault(x => x.Id == item.KpiId);
                        DataContext.KpiTargets.Add(kpiTarget);
                    }
                    i++;
                }
                DataContext.SaveChanges();
                response.IsSuccess = true;
                if (i > 0)
                {
                    response.Message = string.Format("{0}  KPI Target items has been updated successfully", i.ToString());
                }
                else
                {
                    response.Message = "File Successfully Parsed, but no data changed!";
                }
                

            }
            catch (InvalidOperationException invalidOperationException)
            {
                response.Message = invalidOperationException.Message;
            }
            catch (ArgumentNullException argumentNullException)
            {
                response.Message = argumentNullException.Message;
            }
            return response;
        }
    }
}
