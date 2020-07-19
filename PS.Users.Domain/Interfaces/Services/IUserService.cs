using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Services.Base;
using PS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PS.Users.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        UserModel Create(RegisterModel model);
        User Authenticate(string username, string password);
        object GenerateToken(User user);
        User GetById(int id);
    }
}
