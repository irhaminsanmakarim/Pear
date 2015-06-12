using System.Collections.Generic;

namespace DSLNG.PEAR.Services.Responses.User
{
    public class GetUsersResponse : BaseResponse
    {
        public IList<UserResponse> Users { get; set; }
    }
}
