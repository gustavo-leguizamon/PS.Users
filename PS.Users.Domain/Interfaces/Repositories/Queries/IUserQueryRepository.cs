using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Queries.Base;

namespace PS.Users.Domain.Interfaces.Repositories.Queries
{
    public interface IUserQueryRepository : IQuery<User>
    {
    }
}
