using AutoMapper;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Level;
using DSLNG.PEAR.Services.Responses.Level;
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
    public class LevelService : BaseService, ILevelService
    {
        public LevelService(IDataContext dataContext) : base(dataContext)
        {

        }
        public GetLevelResponse GetLevel(GetLevelRequest request)
        {
            var level = DataContext.Levels.First(x => x.Id == request.Id);
            var response = Mapper.Map<GetLevelResponse>(level);

            return response;
        }


        public GetLevelsResponse GetLevels(GetLevelsRequest request)
        {
            var levels = DataContext.Levels.ToList();
            var response = new GetLevelsResponse();
            response.Levels = levels.MapTo<GetLevelsResponse.Level>();
            //response.Levels = levels.MapTo<GetLevelResponse>();

            return response;
        }

        public CreateLevelResponse Create(CreateLevelRequest request)
        {
            var response = new CreateLevelResponse();
            try {
                var level = request.MapTo<Level>();
                DataContext.Levels.Add(level);
                DataContext.SaveChanges();
                response.IsSuccess = true; 
                response.Message = "Level item has been added successfully";
            }
            catch (DbUpdateException dbUpdateException) {
                response.IsSuccess = false;
                response.Message = dbUpdateException.Message;
            }
            return response;
        }

        public UpdateLevelResponse Update(UpdateLevelRequest request)
        {
            var response = new UpdateLevelResponse();
            try {
                var _level = request.MapTo<Level>();
                DataContext.Levels.Attach(_level);
                DataContext.Entry(_level).State = EntityState.Modified;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Measurement item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.IsSuccess = false;
                response.Message = dbUpdateException.Message;
            }
            return response;
        }

        public DeleteLevelResponse Delete(int id)
        {
            var response = new DeleteLevelResponse();
            try {
                var _level = new Level { Id = id};
                DataContext.Levels.Attach(_level);
                DataContext.Entry(_level).State = EntityState.Deleted;
                DataContext.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Measurement item has been updated successfully";
            }
            catch (DbUpdateException dbUpdateException)
            {
                response.IsSuccess = false;
                response.Message = dbUpdateException.Message;
            }
            return response;
        }
    }
}
