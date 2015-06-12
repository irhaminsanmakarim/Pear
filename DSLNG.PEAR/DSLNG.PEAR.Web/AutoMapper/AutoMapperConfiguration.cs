using AutoMapper;
using DSLNG.PEAR.Services;
using DSLNG.PEAR.Services.AutoMapper;

namespace DSLNG.PEAR.Web.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => { 
                cfg.AddProfile(new ServicesMappingProfile());
                cfg.AddProfile(new ViewModelMappingProfile());
            });
        }
    }
}