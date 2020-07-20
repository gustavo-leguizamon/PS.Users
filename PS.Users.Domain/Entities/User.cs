using System.Collections.Generic;

namespace PS.Users.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

        public User()
        {
            UserRole = new HashSet<UserRole>();
        }
    }
}
