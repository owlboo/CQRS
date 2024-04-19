using AutoMapper;
using CQRS.Application.Commands.User;
using CQRS.Application.Interfaces.UoW;
using CQRS.Data;
using FluentValidation;
using MediatR;

namespace CQRS.Application.Handlers.Commands
{
    public class CommandAddUserHandler : IRequestHandler<CommandAddUser, UserDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly IValidator<CommandAddUser> _validator;
        private readonly IMapper _mapper;

        public CommandAddUserHandler(IUnitOfWork uow, IValidator<CommandAddUser> validator, IMapper mapper)
        {
            _uow = uow;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UserDTO> Handle(CommandAddUser request, CancellationToken cancellationToken)
        {
            _validator.ValidateAndThrow(request);

            

            var userDto = _mapper.Map<UserDTO>(request);

            _uow.UserRepository.Add(userDto);
            _uow.SaveChanges();

            return userDto;

        }
    }
}
