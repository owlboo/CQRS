using CQRS.Application.Interfaces.Repositories;
using CQRS.Application.Interfaces.UoW;
using CQRS.Data;

namespace CQRS.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IUserRepository UserRepository { get; }
        private readonly ApplicationDbContext Context;
        public UnitOfWork(IProductRepository ProductRepository,
            IUserRepository UserRepository,
            ApplicationDbContext Context)
        {
            this.ProductRepository = ProductRepository;
            this.UserRepository = UserRepository;
            this.Context = Context;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
