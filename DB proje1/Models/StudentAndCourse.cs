using DB_proje1.Models;

namespace DB_proje1.Models
{
    public class StudentAndCourse
    {
        public Student Student { get; set; }
        public List<Course> Course { get; set; }  
    }
}
