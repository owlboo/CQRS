using CQRS.Application.Interfaces.Repositories;
using CQRS.Data;

namespace CQRS.Repositories
{
    public class ProductRepository : Repository<ProductDTO>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
