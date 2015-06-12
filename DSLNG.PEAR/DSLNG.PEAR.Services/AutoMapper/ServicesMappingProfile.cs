using AutoMapper;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Services.Responses.User;

namespace DSLNG.PEAR.Services.AutoMapper
{
    public class ServicesMappingProfile : Profile
    {
        protected override void Configure()
        {
            //Mapper.CreateMap<GetUserResponse, User>();
            Mapper.CreateMap<User, GetUserResponse>();
            base.Configure();
        }
    }
}

