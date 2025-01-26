namespace SmartCourseSelectorWeb.Models
{
    public class Transcript
    {
        public int TranscriptID { get; set; } 
        public int StudentID { get; set; } 
        public int CourseID { get; set; } 
        public string Grade { get; set; } 
        public string Semester { get; set; } 

        
        public Student Student { get; set; } 
        public Course Course { get; set; } 
    }
}
