using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.RoleGroup;
using System;
using System.Linq;
using DSLNG.PEAR.Common.Extensions;
using DSLNG.PEAR.Services.Responses.RoleGroup;

namespace DSLNG.PEAR.Services
{
    public class RoleGroupService : BaseService, IRoleGroupService
    {
        public RoleGroupService (IDataContext DataContext) : base (DataContext){

        }

        public GetRoleGroupsResponse GetRoleGroups (GetRoleGroupsRequest request){
            var roleGroups = DataContext.RoleGroups.ToList();
            var response = new GetRoleGroupsResponse();
            response.RoleGroups = roleGroups.MapTo<GetRoleGroupsResponse.RoleGroup>();
            return response;
        }

        public GetRoleGroupResponse GetRoleGroup (GetRoleGroupRequest request){
            var response = new GetRoleGroupResponse();
            try {
                var roleGroup = DataContext.RoleGroups.First(x => x.Id == request.Id);
            }catch (ArgumentNullException nullException){
                response.Message = nullException.Message;
            }

            return response;
        }
    }
}
