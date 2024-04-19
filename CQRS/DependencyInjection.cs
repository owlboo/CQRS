using CQRS.Application.AutoMapper;
using CQRS.Application.Behaviors;
using CQRS.Application.Commands.User;
using CQRS.Application.Interfaces.Repositories;
using CQRS.Application.Interfaces.UoW;
using CQRS.Application.Middleware;
using CQRS.Data;
using CQRS.Repositories;
using CQRS.Shared.Optionals;
using CQRS.UoW;
using CQRS.Workers.Consumers;
using CQRS.Workers.Consumers.Definitions;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomizedOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<RabbitOpt>().Bind(configuration.GetSection("Rabbit"));
            return services;
        }
        public static IServiceCollection AddCustomizedDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(cfg =>
                    cfg.UseSqlServer(configuration.GetConnectionString("ProductContext"))
                        .EnableSensitiveDataLogging()
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            );
            return services;
        }

        public static IServiceCollection AddCustomizedAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AutoMapperConfig.RegisterMappings());
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            //services.AddTransient<ExceptionHandlingMiddleware>();
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }

        public static IServiceCollection AddCustomizedMasstransit(this IServiceCollection services, IConfiguration configuration) 
        {

            var rabbitOpt = new RabbitOpt();

            configuration.GetSection("Rabbit").Bind(rabbitOpt);


            services.AddMassTransit(config =>
            {

                //add consumer

                config.AddConsumer<ConsumerAddUser, ConsumerAddUserDefinition>(c =>
                {
                    c.Options<BatchOptions>(batch =>
                    {
                        batch.SetMessageLimit(rabbitOpt.QueueAddUser.BatchSize).SetTimeLimit(s: 1);
                    });
                });

                config.AddConsumer<ConsumerTest>(c =>
                {
                    c.Options<BatchOptions>(batch =>
                    {
                        batch
                        .SetMessageLimit(rabbitOpt.QueueAddUser.BatchSize)
                        .SetTimeLimit(s: 1);
                    });
                });


               

                config.UsingRabbitMq((ctx,cfg) =>
                {
                    cfg.Host(rabbitOpt.Host, rabbitOpt.VirtualHost, h =>
                    {
                        h.Username(rabbitOpt.Username);
                        h.Password(rabbitOpt.Password);
                        h.ConfigureBatchPublish(c => c.Enabled = true);
                        
                    });

                    cfg.UseDelayedMessageScheduler();
                    //cfg.Publish<CommandAddUser>();


                    cfg.ReceiveEndpoint(rabbitOpt.QueueAddUser.Name, ep =>
                    {

                        ep.Durable = rabbitOpt.QueueAddUser.Durable;
                        ep.PrefetchCount = rabbitOpt.QueueAddUser.BatchSize;

                        ep.ConfigureConsumer<ConsumerAddUser>(ctx, c =>
                        {
                            c.ConcurrentMessageLimit = rabbitOpt.QueueAddUser.BatchSize;
                        });
                        

                        if (rabbitOpt.QueueAddUser.BatchSize == 0)
                        {
                            ep.ConfigureConsumeTopology = false; // disabled consume;
                        }
                    });

                    cfg.ReceiveEndpoint("queue-consumer-test", ep =>
                    {

                        ep.Durable = rabbitOpt.QueueAddUser.Durable;
                        ep.PrefetchCount = rabbitOpt.QueueAddUser.BatchSize;

                        ep.ConfigureConsumer<ConsumerTest>(ctx, c =>
                        {
                            c.ConcurrentMessageLimit = rabbitOpt.QueueAddUser.BatchSize;
                        });


                        if (rabbitOpt.QueueAddUser.BatchSize == 0)
                        {
                            ep.ConfigureConsumeTopology = false; // disabled consume;
                        }
                    });

                });             
                
            });


            return services;
        }
    }

    
}
