using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreOnDeleteTest.MOdel
{
    public class Grade
    {
        public int Id { get; set; }
        public String Description { get; set; }

        public IList<Student> Students { get; set; }

    }
}
