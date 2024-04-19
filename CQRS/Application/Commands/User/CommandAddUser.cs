using CQRS.Application.Abstractions.Messaging;
using CQRS.Data;
using MediatR;
using System.Windows.Input;

namespace CQRS.Application.Commands.User
{
    public class CommandAddUser : ICommand<UserDTO>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
