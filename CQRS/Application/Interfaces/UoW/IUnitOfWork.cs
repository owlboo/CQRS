using CQRS.Application.Interfaces.Repositories;

namespace CQRS.Application.Interfaces.UoW
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        int SaveChanges();
    }
}
