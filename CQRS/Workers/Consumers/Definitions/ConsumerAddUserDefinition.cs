using MassTransit;

namespace CQRS.Workers.Consumers.Definitions
{
    public class ConsumerAddUserDefinition : ConsumerDefinition<ConsumerAddUser>
    {

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<ConsumerAddUser> consumerConfigurator, IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(x => x.Interval(10, TimeSpan.FromSeconds(5)));
            endpointConfigurator.UseInMemoryOutbox(context);
        }
    }
}
