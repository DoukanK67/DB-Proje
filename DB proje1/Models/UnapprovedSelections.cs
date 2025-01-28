using DB_proje1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_proje1.Models
{
    public class UnapprovedSelections
    {
        [Key]

        public int ID { get; set; }
        
        public int CourseID { get; set; }
        public int StudentID { get; set; }

        [ForeignKey("StudentID")]
        public Student Student { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
    }
}
