using MediatR;

namespace CQRS.Application.Commands.Product
{
    public class CommandAddProduct : IRequest
    {
        public string MyProperty { get; set; }
    }
}
