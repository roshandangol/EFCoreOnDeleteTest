using EFCoreOnDeleteTest.Data;
using EFCoreOnDeleteTest.Extensions;
using EFCoreOnDeleteTest.MOdel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EFCoreOnDeleteTest
{
    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        };


        private readonly AppSettings _appSettings;

        public EFCoreOnDeleteTestContext _context { get; }

        public UserService(IOptions<AppSettings> appSettings, EFCoreOnDeleteTestContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            
            if (user == null)
                return null;
            var userRole = _context.UserRoles.Where(u => u.UserId == user.Id).FirstOrDefault();
            
            if (userRole == null)
                return null;
            var role = _context.Roles.Where(u => u.Id == userRole.RoleId).FirstOrDefault();
            
            if (role == null)
                return null;

            var userPolicy = _context.Userpolicy.Where(u => u.Id == user.UserPolicyId).FirstOrDefault();
            if (userPolicy == null)
                return null;


            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            ////set the time when it expires
            //DateTime expires = DateTime.UtcNow.AddMinutes(userPolicy.SessionAccessDuration);

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                NotBefore = issuedAt,
                Expires = DateTime.UtcNow.AddMinutes(userPolicy.SessionAccessDuration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "http://localhost:56538",
                Audience = "http://localhost:56538",                
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            _context.SaveChanges();

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.WithoutPasswords();
            //return _users.WithoutPasswords();
        }

        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            return user.WithoutPassword();
        }
    }
}
