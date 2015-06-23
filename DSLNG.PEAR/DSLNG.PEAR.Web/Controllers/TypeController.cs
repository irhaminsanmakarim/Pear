using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.Type;
using DSLNG.PEAR.Services.Requests.Type;

namespace DSLNG.PEAR.Web.Controllers
{
    public class TypeController : BaseController
    {
        private readonly ITypeService _typeService;

        public TypeController(ITypeService service)
        {
            _typeService = service;
        }

        public ActionResult Index()
        {
            var response = _typeService.GetTypes(new GetTypesRequest());
            var viewModel = new IndexTypeViewModel();
            viewModel.Types = response.Types.MapTo<TypeViewModel>();

            return View(viewModel);
        }
    }
}