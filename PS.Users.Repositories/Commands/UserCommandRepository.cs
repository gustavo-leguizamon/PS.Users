using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Commands;
using PS.Users.Repositories.Commands.Base;
using PS.Users.Repositories.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Repositories.Commands
{
    public class UserCommandRepository : CommandRepository<User>, IUserCommandRepository
    {
        public UserCommandRepository(UsersContext context) : base(context)
        {
        }
    }
}
