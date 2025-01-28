using DB_proje1.Models;
using System.ComponentModel.DataAnnotations;

namespace DB_proje1.Models
{
    public class SelectionHistory
    {
        [Key]
        public int StudentID { get; set; }  
        public DateTime SelectionDate { get; set; }  

     
        public virtual Student? Student { get; set; }
    }
}
