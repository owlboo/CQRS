using CQRS.Application.Interfaces.Repositories;
using CQRS.Data;

namespace CQRS.Repositories
{
    public class UserRepository : Repository<UserDTO>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
