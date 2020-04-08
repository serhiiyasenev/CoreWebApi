using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Contexts;
using StudentsApi.Entities;
using StudentsApi.Students.Models;

namespace StudentsApi.Students.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
   
    public class StudentController : ControllerBase
    {
        private readonly StudentsDbContext _dbContext;

        public StudentController(StudentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {

                StudentEntity[] students = await _dbContext.Students.AsNoTracking()
                                                    //.Include(t => t.Teachers)
                                                    //.ThenInclude(st => st.Teacher)
                                                    .ToArrayAsync();

                // if we include teachers, then there is a loop because teachers have a link to students

                return new ObjectResult(students);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                // if we include teachers, then there is a cycle because teachers have a link to students

                var student = await _dbContext.Students.AsNoTracking()
                    //.Include(t => t.Teachers)
                    //.ThenInclude(st => st.Teacher.Discipline)
                    .FirstOrDefaultAsync(st => st.Id.Equals(id));


                if (student == null)
                {
                    return NotFound();
                }

                return new ObjectResult(student);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentModel model)
        {
            try
            {
                if (model.Score.IsScoreAcceptable().Item1)
                {
                    return BadRequest(model.Score.IsScoreAcceptable().Item2);
                }

                var teachers = new List<StudentTeacherEntity>();

                foreach (var item in model.Teachers)
                {
                    teachers.Add(new StudentTeacherEntity
                    {
                        Teacher = new TeacherEntity
                        {
                            Name = item.Name,
                            Discipline = item.Discipline
                        }
                    });
                }

                var student = _dbContext.Students
                    .Add(new StudentEntity { Name = model.Name, Score = model.Score, Teachers = teachers });
                
                await _dbContext.SaveChangesAsync();
                
                return Ok(student.Entity);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudentById([FromQuery] int id,
                                                           [FromBody] StudentModel model)
        {
            try
            {
                var student = await _dbContext.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                if (model.Score.IsScoreAcceptable().Item1)
                {
                    return BadRequest(model.Score.IsScoreAcceptable().Item2);
                }

                student.Score = model.Score;

                student.Name = model.Name;


                await _dbContext.SaveChangesAsync();

                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpPatch("{id:int}, {score:int}")]
        public async Task<IActionResult> PatchScoreForStudentById([FromQuery] int id,
                                                                  [FromQuery] int score)
        {
            try
            {
                var student = await _dbContext.Students.FindAsync(id);
        
                if (student == null)
                {
                    return NotFound();
                }
        
                if (score.IsScoreAcceptable().Item1)
                {
                    return BadRequest(score.IsScoreAcceptable().Item2);
                }
        
                student.Score = score;
        
                await _dbContext.SaveChangesAsync();
        
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            try
            {
                var student = await _dbContext.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }
                _dbContext.Students.Remove(student);

                await _dbContext.SaveChangesAsync();

                return Ok($"Student with id {student.Id} and name {student.Name} was deleted");
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }
    }
}
