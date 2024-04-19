using CQRS.Application.Interfaces.UoW;
using CQRS.Application.Queries.User;
using CQRS.Data;
using MediatR;

namespace CQRS.Application.Handlers.Queries
{
    public class QueryGetUserByIdHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUnitOfWork _uow;
        public QueryGetUserByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public async Task<UserDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _uow.UserRepository.FindById(request.Id);
            return user;
        }
    }
}
