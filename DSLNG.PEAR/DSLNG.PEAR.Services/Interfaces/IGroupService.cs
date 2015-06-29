using DSLNG.PEAR.Services.Requests.Group;
using DSLNG.PEAR.Services.Responses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IGroupService
    {
        GetGroupResponse GetGroup(GetGroupRequest request);
        GetGroupsResponse GetGroups(GetGroupsRequest request);
        CreateGroupResponse Create(CreateGroupRequest request);
        UpdateGroupResponse Update(UpdateGroupRequest request);
        DeleteGroupResponse Delete(int id);
    }
}
