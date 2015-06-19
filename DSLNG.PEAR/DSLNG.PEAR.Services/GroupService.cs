using DSLNG.PEAR.Data.Persistence;
using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.Group;
using DSLNG.PEAR.Services.Responses.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }
    }
}
