using AutoMapper;
using CQRS.Application.Commands.User;
using CQRS.Application.Interfaces.UoW;
using CQRS.Data;
using MediatR;

namespace CQRS.Application.Handlers.Commands
{
    public class CommandUpdateUserHandler : IRequestHandler<CommandUpdateUser, UserDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public CommandUpdateUserHandler(IUnitOfWork uow,
            IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<UserDTO> Handle(CommandUpdateUser request, CancellationToken cancellationToken)
        {
            // validate request here

            var user = _uow.UserRepository.FindById(Guid.Parse(request.Id));
            if (user == null)
            {
                throw new Exception("Not found user");
            }

            user = _mapper.Map<UserDTO>(request);
            _uow.UserRepository.UpdateOne(user);
            _uow.SaveChanges();
            return user;

        }
    }
}
