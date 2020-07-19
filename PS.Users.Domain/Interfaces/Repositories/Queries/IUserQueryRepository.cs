using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Queries.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Domain.Interfaces.Repositories.Queries
{
    public interface IUserQueryRepository : IQuery<User>
    {
    }
}
