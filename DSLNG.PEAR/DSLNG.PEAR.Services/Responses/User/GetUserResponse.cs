


namespace DSLNG.PEAR.Services.Responses.User
{
    public class GetUserResponse : BaseResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
    }
}
