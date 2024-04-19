using CQRS.Data;
using MediatR;

namespace CQRS.Application.Commands.User
{
    public class CommandUpdateUser : IRequest<UserDTO>
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
