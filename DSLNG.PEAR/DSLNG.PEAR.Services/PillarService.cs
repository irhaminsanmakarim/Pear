using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Pillar;
using DSLNG.PEAR.Services.Responses.Pillar;
using DSLNG.PEAR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DSLNG.PEAR.Common.Extensions;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace DSLNG.PEAR.Services
{
    public class PillarService : BaseService, IPillarService
    {
        public PillarService(IDataContext dataContext): base(dataContext)
        {

        }
        public GetPillarResponse GetPillar(GetPillarRequest request)
        {
            try
            {
                var pillar = DataContext.Pillars.First(x => x.Id == request.Id);
                var response = pillar.MapTo<GetPillarResponse>(); 

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetPillarResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }

        public GetPillarsResponse GetPillars(GetPillarsRequest request)
        {
            var pillars = DataContext.Pillars.ToList();
            var response = new GetPillarsResponse();
            response.Pillars = pillars.MapTo<GetPillarsResponse.Pillar>();

            return response;
        }

        public CreatePillarResponse Create(CreatePillarRequest request)
        {
            var response = new CreatePillarResponse();
            try
            {
                var pillar = request.MapTo<Pillar>();
                DataContext.Pillars.Add(pillar);
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pillar item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public UpdatePillarResponse Update(UpdatePillarRequest request)
        {
            var response = new UpdatePillarResponse();
            try
            {
                var pillar = request.MapTo<Pillar>();
                DataContext.Pillars.Attach(pillar);
                DataContext.Entry(pillar).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pillar item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }

        public DeletePillarResponse Delete(int id)
        {
            var response = new DeletePillarResponse();
            try
            {
                var pillar = new Pillar { Id = id };
                DataContext.Pillars.Attach(pillar);
                DataContext.Entry(pillar).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Pillar item has been deleted successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.Message = dbUpdateException.Message;
            }

            return response;
        }
    }
}
