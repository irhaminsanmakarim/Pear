using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Group;
using DSLNG.PEAR.Services.Responses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Common.Extensions;

namespace DSLNG.PEAR.Services
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(IDataContext dataContext)
            : base(dataContext)
        {

        }

        public GetGroupResponse GetGroup(GetGroupRequest request)
        {
            try
            {
                var group = DataContext.Groups.First(x => x.Id == request.Id);
                var response = group.MapTo<GetGroupResponse>();

                return response;
            }
            catch (System.InvalidOperationException x)
            {
                return new GetGroupResponse
                {
                    IsSuccess = false,
                    Message = x.Message
                };
            }
        }


        public GetGroupsResponse GetGroups(GetGroupsRequest request)
        {
            var response = new GetGroupsResponse();
            var groups = DataContext.Groups.ToList();
            response.Groups = groups.MapTo<GetGroupsResponse.Group>();
            return response;
        }
    }
}
