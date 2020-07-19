using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PS.Users.Domain.Entities;
using PS.Users.Domain.Interfaces.Repositories.Commands;
using PS.Users.Domain.Interfaces.Repositories.Queries;
using PS.Users.Business.Services.Base;
using PS.Users.Domain.Interfaces.Services;
using PS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PS.Users.Business.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(IUserCommandRepository command, IUserQueryRepository query, IMapper mapper) : base(command, query, mapper)
        {
        }

        public UserModel Create(RegisterModel model)
        {
            var user = Mapper.Map<User>(model);

            // validation
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new Exception("Se requiere contraseña");

            if (Query.Exists(x => x.Username == user.Username))
                throw new Exception($"El usuario {user.Username} ya existe");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            user.UserRole.Add(new UserRole { RoleId = model.RoleId });

            Command.Insert(user);

            return Mapper.Map<UserModel>(user);
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                throw new Exception("Se requiere usuario y contraseña");

            var user = Query.FindBy(x => x.Username == username, new string[] { "UserRole", "UserRole.Role" }).FirstOrDefault();

            // check if username exists
            if (user == null)
                throw new Exception("Usuario o contraseña incorrectos");

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new Exception("Usuario o contraseña incorrectos");

            // authentication successful
            return user;
        }

        public object GenerateToken(User user)
        {
            var secret = "V4cCI6MTU5NTI2MDY2NCwiaWF0IjoxNT"; //_appSettings.Secret
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.UserRole.First().Role.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new
            {
                Username = user.Username,
                Token = tokenString
            };
        }

        public User GetById(int id)
        {
            return Query.FindById(id);
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Se debe proporcionar un valor.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Se debe proporcionar un valor.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Longitud invalida del password hash (64 bytes esperados).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Longitud invalida del password salt (128 bytes esperados).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
