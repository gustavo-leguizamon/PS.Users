using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Queries;
using PS.Users.Repositories.Contexts;
using PS.Users.Repositories.Queries.Base;

namespace PS.Users.Repositories.Queries
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(UsersContext context) : base(context)
        {
        }
    }
}
