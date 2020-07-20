using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Commands.Base;

namespace PS.Users.Domain.Interfaces.Repositories.Commands
{
    public interface IUserCommandRepository : ICommand<User>
    {
    }
}
