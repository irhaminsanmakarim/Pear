using AutoMapper;
using DSLNG.PEAR.Services.Responses.Level;
using DSLNG.PEAR.Services.Responses.Menu;
using DSLNG.PEAR.Web.ViewModels.Level;
using DSLNG.PEAR.Web.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSLNG.PEAR.Web.AutoMapper
{
    public class ViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<GetLevelResponse, LevelViewModel>();
            Mapper.CreateMap<GetMenuResponse, MenusViewModel>();
            base.Configure();
        }
    }
}