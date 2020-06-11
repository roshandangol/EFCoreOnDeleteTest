using EFCoreOnDeleteTest.MOdel;
using System.Collections.Generic;

namespace EFCoreOnDeleteTest
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
