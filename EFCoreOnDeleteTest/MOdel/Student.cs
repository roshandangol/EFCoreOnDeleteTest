using System;

namespace EFCoreOnDeleteTest.MOdel
{
    public class Student
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int? GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
