namespace CQRS.Application.Requests.User
{
    public class UpdateUserRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
