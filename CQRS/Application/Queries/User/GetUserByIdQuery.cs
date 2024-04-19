using CQRS.Data;
using MediatR;

namespace CQRS.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<UserDTO>
    {
        public Guid Id { get; set; }
    }
}
