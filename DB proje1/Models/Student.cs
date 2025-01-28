using DB_proje1.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_proje1.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? AdvisorID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Department { get; set; } 

  
        public Advisor Advisor { get; set; }

        [InverseProperty("Student")]
        public ICollection<StudentCourseSelection>? StudentCourseSelections { get; set; } = new List<StudentCourseSelection>();
        [InverseProperty("Student")]
        public ICollection<UnapprovedSelections>? UnapprovedSelections {  get; set; }=new List<UnapprovedSelections>(); 
    }
}
