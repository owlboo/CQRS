using CQRS.Application.Commands.User;
using CQRS.Application.Interfaces.UoW;
using MassTransit;
using MediatR;

namespace CQRS.Workers.Consumers
{
    public class ConsumerAddUser : IConsumer<Batch<CommandAddUser>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediator _mediator;
        public ConsumerAddUser(IUnitOfWork uow,
            IMediator mediator)
        {
            _uow = uow;
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<Batch<CommandAddUser>> context)
        {
            var commandAddMultipleUser = new CommandAddMultipleUser();
            commandAddMultipleUser.Users = context.Message.Select(x => x.Message).ToList();

            var result = await _mediator.Send(commandAddMultipleUser);

        }
    }
}
