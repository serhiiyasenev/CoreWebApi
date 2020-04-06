using System;
using System.Linq;
using System.Threading.Tasks;
using CoreWebApp.Contexts;
using CoreWebApp.Students.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CoreWebApp.Teachers.Controllers
{
    [Authorize]
    [Route("[controller]")]
   
    public class TeacherController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TeacherController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllTeacher")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _context.Students.ToArrayAsync();
                return new ObjectResult(students.OrderBy(s => s.Name));
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetTeacherById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _context.Students.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                return new ObjectResult(item);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpPost(Name = "PostTeacher")]
        public async Task<IActionResult> Post([FromBody] Student model)
        {
            try
            {

                if (model.Score > 100)
                {
                    return BadRequest("Wrong score: " + model.Score + "cannot be more than 100");
                }

                if (model.Score < 0)
                {
                    return BadRequest("Wrong score: " + model.Score + "cannot be less than 0");
                }

                EntityEntry<Student> student = _context.Students.Add(new Student { Name = model.Name, Score = model.Score });
                await _context.SaveChangesAsync();
                return Ok(student.Entity);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpPost("{id:int}, {score:int}", Name = "PatchTeacher")]
        public async Task<IActionResult> Patch([FromQuery] int id, [FromQuery] int score)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                student.Score = score;

                await _context.SaveChangesAsync();
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteTeacherById")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);
                if (student == null)
                {
                    return NotFound();
                }
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return Ok(student.Name);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }
    }
}
