using CQRS.Application.Requests.User;
using CQRS.Data;
using MediatR;

namespace CQRS.Application.Commands.User
{
    public class CommandAddMultipleUser: IRequest<IEnumerable<UserDTO>>
    {
        public List<CommandAddUser> Users { get; set; }
        public CommandAddMultipleUser()
        {
            Users = new List<CommandAddUser>();
        }
    }
}
