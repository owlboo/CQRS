using CQRS.Application.Commands.User;
using MassTransit;

namespace CQRS.Workers.Consumers
{
    public class ConsumerTest : IConsumer<Batch<CommandAddUser>>
    {

        public Task Consume(ConsumeContext<Batch<CommandAddUser>> context)
        {
            Console.WriteLine("Message received : {0}", context.Message.Count());

            return Task.CompletedTask;
        }
    }
}
