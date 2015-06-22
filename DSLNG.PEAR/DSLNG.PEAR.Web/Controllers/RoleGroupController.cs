using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using System.Web.Mvc;
using DSLNG.PEAR.Web.ViewModels.RoleGroup;

namespace DSLNG.PEAR.Web.Controllers
{
    public class RoleGroupController : BaseController
    {
        private readonly IRoleGroupService _roleGroupService;

        public RoleGroupController(IRoleGroupService service)
        {
            _roleGroupService = service;
        }

        public ActionResult Index()
        {
            var response = _roleGroupService.GetRoleGroups(new GetRoleGroupsRequest());
            var viewModel = new IndexRoleGroupViewModel();
            viewModel.RoleGroups = response.RoleGroups.MapTo<RoleGroupViewModel>();

            return View(viewModel);
        }
    }
}