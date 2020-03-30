﻿using FirstWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FirstWebApplication.Controllers
{
    [Authorize]
    [Route("[controller]")]
   
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public StudentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var students = await _context.Students.ToArrayAsync();
                return new ObjectResult(students.OrderBy(s => s.Id));
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }

        [HttpGet("{id:int}", Name = "GetItemById")]
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

        [HttpPost(Name = "Post")]
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

        [HttpPost("{id:int}, {score:int}", Name = "Patch")]
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

        [HttpDelete("{id:int}", Name = "DeletetemById")]
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
                return Ok(student.Id);
            }
            catch (Exception e)
            {
                return BadRequest("Wrong request: " + e.Message);
            }
        }
    }
}
