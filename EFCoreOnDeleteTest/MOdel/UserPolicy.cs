using System.Collections.Generic;

namespace EFCoreOnDeleteTest.MOdel
{
    public class UserPolicy
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int SessionAccessDuration { get; set; }
        public ICollection<User> Users { get; set; }
    }

}
