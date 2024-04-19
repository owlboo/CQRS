using Bogus;
using CQRS.Application.Commands.User;
using CQRS.Application.Queries.User;
using CQRS.Application.Requests.User;
using CQRS.Data;
using CQRS.Shared.Optionals;
using FakeItEasy;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IBus _bus;
        private readonly IOptions<RabbitOpt> _rabbitOpt;
        public UserController(IMediator mediator,
            ISendEndpointProvider sendEndpointProvider,
            IOptions<RabbitOpt> rabbitOpt,
            IBus bus)
        {
            _mediator = mediator;
            _sendEndpointProvider = sendEndpointProvider;
            _rabbitOpt = rabbitOpt;
            _bus = bus;
        }


        [HttpGet]
        [Route("get/id")]
        public async Task<IActionResult> GetById(string id)
        {
            var getUserByIdQuery = new GetUserByIdQuery { Id = Guid.Parse(id) };
            var userDto = await _mediator.Send(getUserByIdQuery);
            return Ok(userDto);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddUser([FromBody]AddUserRequest req)
        {

            var addUserCommand = new CommandAddUser()
            {
                Email = req.Email,
                Address = req.Address,
                Username = req.Username
            };

            var result = await _mediator.Send(addUserCommand);


            return new JsonResult(result);
        }

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> AddMultipleUser([FromBody] IEnumerable<AddUserRequest> req)
        {

            //CommandAddMultipleUser cmd = new CommandAddMultipleUser();

            var faker = new Faker<CommandAddUser>()
                .RuleFor(r => r.Address, e => e.Address.FullAddress())
                .RuleFor(r => r.Username, e => e.Name.LastName())
                .RuleFor(r => r.Email, e => e.Internet.Email());

            var collectionFake = faker.Generate(10);

            //cmd.Users = collectionFake;


            var ep = await _sendEndpointProvider.GetSendEndpoint(_rabbitOpt.Value.QueueAddUser.GetSendEndpoint());


            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

            await ep.SendBatch(collectionFake,cts.Token);

            //var result = await _mediator.Send(cmd);

            //var ep = await _publishEndpoint.pub.GetPublishSendEndpoint<CommandAddUser>();

            //await ep.SendBatch(collectionFake);


            return new JsonResult(true);
        }

        [HttpPut]
        [Route("/id")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] AddUserRequest req)
        {

            var updateUserCommand = new CommandUpdateUser()
            {
                Id = id,
                Email = req.Email,
                Address = req.Address,
                Username = req.Username
            };

            var result = await _mediator.Send(updateUserCommand);

            return new JsonResult(result);
        }

        
    }
}
