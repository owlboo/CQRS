using CQRS.Data;

namespace CQRS.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<UserDTO>
    {
    }
}
