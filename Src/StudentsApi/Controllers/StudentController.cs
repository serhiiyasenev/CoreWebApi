using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Contexts;
using StudentsApi.Entities;
using StudentsApi.Helpers;
using StudentsApi.Models;

namespace StudentsApi.Controllers
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
                var studentEntities = await _dbContext.Students.AsNoTracking()
                                                    .Include(t => t.Group)
                                                    .ThenInclude(g => g.GroupName)
                                                    .ToListAsync();

                var students = new List<StudentModel>();

                foreach (var entity in studentEntities)
                {
                    var student = new StudentModel
                    {
                        StudentName = entity.StudentName, 
                        AvrScore = entity.AvrScore, 
                       GroupName = entity.Group.GroupName
                    };
                    
                    students.Add(student);
                }

                var jsonModel = JsonHelper.FromObjectToJson(students);


                List<StudentModel> test = JsonHelper.FromJsonToObject<List<StudentModel>>(jsonModel);

                string test2 = JsonHelper.FromObjectToJson(jsonModel);

                return new ObjectResult(jsonModel);
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
                var entity = await _dbContext.Students.AsNoTracking()
                    .Include(t => t.Group)
                    .ThenInclude(g => g.GroupName)
                    .FirstOrDefaultAsync(st => st.StudentId.Equals(id));


                if (entity == null)
                {
                    return NotFound();
                }

                var student = new StudentModel
                {
                    StudentName = entity.StudentName,
                    AvrScore = entity.AvrScore,
                    Course = entity.Group.Course,
                    GroupName = entity.Group.GroupName
                };


                var json = JsonHelper.FromObjectToJson(student);

                return new ObjectResult(json);
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
                if (!model.AvrScore.IsScoreAcceptable().Item1)
                {
                    return BadRequest(model.AvrScore.IsScoreAcceptable().Item2);
                }


                var student = _dbContext.Students
                    .Add(new StudentEntity
                    {
                        StudentName = model.StudentName, 
                        AvrScore = model.AvrScore, 
                        Group = new GroupEntity
                        {
                            GroupName = model.GroupName,
                            Course = model.Course
                        }
                    });
                
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

                if (!model.AvrScore.IsScoreAcceptable().Item1)
                {
                    return BadRequest(model.AvrScore.IsScoreAcceptable().Item2);
                }

                student.AvrScore = model.AvrScore;

                student.StudentName = model.StudentName;


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
        
                if (!score.IsScoreAcceptable().Item1)
                {
                    return BadRequest(score.IsScoreAcceptable().Item2);
                }
        
                student.AvrScore = score;
        
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

                return Ok($"Student with id {student.StudentId} and name {student.StudentName} was deleted");
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }
    }
}
