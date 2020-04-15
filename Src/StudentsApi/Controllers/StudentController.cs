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
    [Route("api/student")]

    public class StudentController : ControllerBase
    {
        private readonly StudentsDbContext _dbContext;

        public StudentController(StudentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet(Name = nameof(GetAllStudents))]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var studentEntities = await _dbContext.Students.AsNoTracking()
                                                    .Include(d => d.Disciplines)
                                                    .ThenInclude(n => n.DisciplineName)
                                                    .ToListAsync();

                var students = (from entity in studentEntities 
                    let disciplines = entity.Disciplines
                        .Select(discipline => new DisciplineModel
                        {
                            DisciplineName = discipline.DisciplineName
                        }).ToList() 
                    select new StudentModel
                    {
                        StudentName = entity.StudentName, 
                        Disciplines = disciplines
                    }).ToList();


                var jsonModel = JsonHelper.FromObjectToJson(students);


                var test = JsonHelper.FromJsonToObject<List<StudentModel>>(jsonModel);

                var test2 = JsonHelper.FromObjectToJson(jsonModel);

                return new ObjectResult(jsonModel);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.InnerException);
            }
        }

        [HttpGet("{id:int}", Name = nameof(GetStudentById))]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var entity = await _dbContext.Students.AsNoTracking()
                    .Include(d => d.Disciplines)
                    .ThenInclude(n => n.DisciplineName)
                    .FirstOrDefaultAsync(st => st.StudentId.Equals(id));


                if (entity == null)
                {
                    return NotFound();
                }

                var disciplines = new List<DisciplineModel>();

                foreach (var discipline in entity.Disciplines)
                {
                    disciplines.Add(new DisciplineModel
                    {
                        DisciplineName = discipline.DisciplineName
                    });
                }

                var student = new StudentModel
                {
                    StudentId = entity.StudentId,
                    StudentName = entity.StudentName,
                    Disciplines = disciplines
                };


                var json = JsonHelper.FromObjectToJson(student);

                return new ObjectResult(json);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.InnerException);
            }
        }

        [HttpPost(Name = nameof(CreateStudent))]
        public async Task<IActionResult> CreateStudent([FromBody] StudentModel model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest();
                }

                var disciplines = model.Disciplines
                    .Select(d =>
                        new DisciplineEntity
                        {
                            DisciplineName = d.DisciplineName
                        }).ToList();


                var student = _dbContext.Students.Add(
                    new StudentEntity
                    {
                        StudentName = model.StudentName,
                        Disciplines = disciplines
                    });
                
                await _dbContext.SaveChangesAsync();
                
                return Ok(student.Entity);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.InnerException);
            }
        }

        [HttpDelete("{id:int}", Name = nameof(DeleteStudentById))]
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
                return BadRequest("Wrong request: " + e.InnerException);
            }
        }
    }
}
