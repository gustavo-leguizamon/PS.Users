using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Queries;
using PS.Users.Repositories.Contexts;
using PS.Users.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Repositories.Queries
{
    public class UserQueryRepository : QueryRepository<User>, IUserQueryRepository
    {
        public UserQueryRepository(UsersContext context) : base(context)
        {
        }
    }
}
