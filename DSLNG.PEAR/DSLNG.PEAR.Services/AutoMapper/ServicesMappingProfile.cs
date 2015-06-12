using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.User;

namespace DSLNG.PEAR.Services.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<User, GetUserResponse>();
            Mapper.CreateMap<Level, GetLevelResponse>();
            base.Configure();
        }
    }
}

