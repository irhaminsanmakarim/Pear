using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Responses.Group
{
    public class GetGroupResponse : BaseResponse
    {
        //public class Group {
            public int Id { get; set; }

            public string Name { get; set; }
            public int? Order { get; set; }
            public string Remark { get; set; }
            //public Activity Activity { get; set; }
            public bool IsActive { get; set; }
        //}

        //public class Activity
        //{
        //    public int Id { get; set; }

        //    public int Order { get; set; }
        //    public string Remark { get; set; }
        //    public bool IsActive { get; set; }
        //}
    }
}
