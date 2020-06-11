using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EFCoreOnDeleteTest.MOdel
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Token { get; set; }

        public int? UserPolicyId { get; set; }
        public UserPolicy UserPolicy { get; set; }

        public IList<UserRole> UserRoles { get; set; }
    }

    public class UserRole
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

    }

    public class Role
        {
            public int Id { get; set; }
            public string RoleName { get; set; }
            public IList<UserRole> UserRoles { get; set; }

    }

}
