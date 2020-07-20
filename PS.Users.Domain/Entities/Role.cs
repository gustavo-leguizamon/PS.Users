using System.Collections.Generic;

namespace PS.Users.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }

        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }
    }
}
