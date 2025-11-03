using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Contexts;
using StudentsApi.Entities;
using StudentsApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsApi.Controllers
{
    [ApiController]
    [Route("api/student")]

    public class StudentController(StudentsDbContext dbContext) : ControllerBase
    {
        [HttpGet(Name = nameof(GetAllStudents))]
        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var studentEntities = await dbContext.Students.AsNoTracking()
                                                    .Include(d => d.Disciplines)
                                                    .ToListAsync();

                var students = studentEntities.Select(entity 
                => new StudentModel
                {
                    Name = entity.Name,
                    Disciplines = entity.Disciplines
                }).ToList();

                var outboundModel = JsonHelper.FromObjectToJson(students);

                return new ObjectResult(outboundModel);
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request: {e.Message}");
            }
        }

        [HttpGet("{id:int}", Name = nameof(GetStudentById))]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var entity = await dbContext.Students.AsNoTracking()
                                                      .Include(d => d.Disciplines)
                                                      .FirstOrDefaultAsync(st => st.Id.Equals(id));

                if (entity == null)
                {
                    return NotFound();
                }

                var student = new StudentModel
                {
                    Name = entity.Name,
                    Disciplines = entity.Disciplines
                };

                var outboundModel = JsonHelper.FromObjectToJson(student);

                return new ObjectResult(outboundModel);
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request: {e.Message}");
            }
        }

        [HttpPost(Name = nameof(CreateStudent))]
        public async Task<IActionResult> CreateStudent([FromBody] StudentModel inboundModel)
        {
            try
            {
                if (inboundModel == null)
                {
                    return BadRequest();
                }

                var disciplines = new HashSet<DisciplineEntity>();

                foreach (var d in inboundModel.Disciplines)
                {
                    disciplines.Add(d);
                }

                var studentToAdd = new StudentEntity
                {
                    Name = inboundModel.Name,
                    Disciplines = disciplines
                };

                var studentEntity = await dbContext.Students.AddAsync(studentToAdd);

                await dbContext.SaveChangesAsync();
                
                return Created(studentEntity.Entity.Id.ToString(), studentEntity.Entity);
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request: {e.Message}");
            }
        }

        [HttpDelete("{id:int}", Name = nameof(DeleteStudentById))]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            try
            {
                var student = await dbContext.Students.FindAsync(id);

                if (student == null)
                {
                    return NotFound();
                }

                dbContext.Students.Remove(student);

                await dbContext.SaveChangesAsync();

                return Ok($"Student with id {student.Id} and name {student.Name} was deleted");
            }
            catch (Exception e)
            {
                return BadRequest($"Wrong request: {e.Message}");
            }
        }
    }
}