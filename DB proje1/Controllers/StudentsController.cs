using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_proje1.Models;
using DB_proje1.Models;
using System;

namespace DB_proje1.Controllers
{
    [Route("api/StudentController")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("SelectCourses")]
        public async Task<IActionResult> SelectCourses([FromQuery] int StudentId)
        {
            if (StudentId == 0)
            {
                return BadRequest("Invalid student ID.");
            }

            var course = await _context.Courses.ToListAsync();
            var student = await _context.Students
                                       .Include(s => s.StudentCourseSelections)
                                           .ThenInclude(sc => sc.Course)
                                       .Include(s => s.Advisor)
                                       .FirstOrDefaultAsync(s => s.StudentID == StudentId);

            if (student == null)
            {
                return NotFound($"Student with ID {StudentId} not found.");
            }

            var sc = new StudentAndCourse
            {
                Course = course,
                Student = student
            };

            return View(sc);
        }


        [HttpPost("SubmitSelectedCourses")]
        public async Task<IActionResult> SubmitSelectedCourses([FromBody] SubmitCoursesRequest request)
        {
            if (request == null || request.SelectedCourseIds == null || !request.SelectedCourseIds.Any())
            {
                return BadRequest("No courses selected.");
            }

            try
            {
                
                var existingRecords = _context.UnapprovedSelections
                    .Where(x => x.StudentID == request.StudentId)
                    .ToList();

                if (existingRecords.Any())
                {
                    
                    foreach (var record in existingRecords)
                    {
                        var course = await _context.Courses.FindAsync(record.CourseID);
                        if (course != null)
                        {
                            course.Quota += 1; 
                        }
                    }

                    _context.UnapprovedSelections.RemoveRange(existingRecords);
                }

                
                int maxId = _context.UnapprovedSelections.Any()
                    ? _context.UnapprovedSelections.Max(x => x.ID)
                    : 0;

                foreach (var courseId in request.SelectedCourseIds)
                {
                    maxId++;

                    var unapprovedSelection = new UnapprovedSelections
                    {
                        ID = maxId,
                        StudentID = request.StudentId,
                        CourseID = courseId
                    };

                    
                    var course = await _context.Courses.FindAsync(courseId);
                    if (course != null)
                    {
                        if (course.Quota > 0)
                        {
                            course.Quota -= 1; 
                        }
                        else
                        {
                            return BadRequest($"The course {course.CourseName} is full and cannot be selected.");
                        }
                    }

                    _context.UnapprovedSelections.Add(unapprovedSelection);
                }

                var existingStudentRecords = _context.StudentCourseSelections
                                .Where(x => x.StudentID == request.StudentId)
                                .ToList();

                if (existingStudentRecords.Any())
                {
                    _context.StudentCourseSelections.RemoveRange(existingStudentRecords);
                }

                
                foreach (var courseId in request.SelectedCourseIds)
                {
                    var studentCourseSelection = new StudentCourseSelection
                    {
                        StudentID = request.StudentId,
                        CourseID = courseId,
                        IsApproved = false,
                        SelectionDate = DateTime.Now
                    };

                    _context.StudentCourseSelections.Add(studentCourseSelection);
                }

              
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Courses updated successfully!" });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new { Message = "An error occurred while updating courses.", Details = ex.Message });
            }
        }



        [HttpGet("CourseSelection")]
        public async Task<IActionResult> CourseSelection(int id)
        {

            var student = await _context.Students
                                         .Include(s => s.StudentCourseSelections) 
                                             .ThenInclude(sc => sc.Course)       
                                         .Include(s => s.Advisor)                
                                         .Include(s => s.UnapprovedSelections)   
                                             .ThenInclude(us => us.Course)      
                                         .FirstOrDefaultAsync(s => s.StudentID == id);

            
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            
            return View(student);

        }



        
        [HttpGet("getStudentList")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        
        [HttpGet("getById")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound(new { Message = "Student not found." });
            }

            return student;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest(new { Message = "Student ID mismatch." });
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound(new { Message = "Student not found." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest(new { Message = "Student data is invalid." });
            }

            
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction("GetStudent", new { id = student.StudentID }, student);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound(new { Message = "Student not found." });
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
       
        [HttpGet("getByAdvisorId")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByAdvisorId(int advisorId)
        {
            
            var students = await _context.Students
                                          .Where(s => s.AdvisorID == advisorId) 
                                          .ToListAsync();

            
            if (!students.Any())
            {
                return NotFound(new { Message = $"No students found for AdvisorID {advisorId}." });
            }

            
            return Ok(students);
        }

    }
}