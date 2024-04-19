using CQRS.Data;

namespace CQRS.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<ProductDTO>
    {
    }
}
