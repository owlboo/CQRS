using AutoMapper;
using CQRS.Application.Commands.User;
using CQRS.Application.Interfaces.UoW;
using CQRS.Data;
using MediatR;

namespace CQRS.Application.Handlers.Commands
{
    public class CommandAddMultpleUserHandler : IRequestHandler<CommandAddMultipleUser, IEnumerable<UserDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CommandAddMultpleUserHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDTO>> Handle(CommandAddMultipleUser request, CancellationToken cancellationToken)
        {
            // validate command here

            var userDto = _mapper.Map<IEnumerable<UserDTO>>(request.Users);

            var result = _uow.UserRepository.AddMany(userDto);

            return result;

        }
    }
}
