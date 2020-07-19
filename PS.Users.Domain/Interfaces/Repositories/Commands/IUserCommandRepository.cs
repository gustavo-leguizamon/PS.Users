using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Domain.Interfaces.Repositories.Commands
{
    public interface IUserCommandRepository : ICommand<User>
    {
    }
}
