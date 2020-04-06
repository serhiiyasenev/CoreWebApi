using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CoreWebApp.Contexts;
using CoreWebApp.Students.Models;
using CoreWebApp.Teachers.Models;
using Microsoft.AspNetCore.Http;

namespace CoreWebApp.Students.StudentsMeddleware
{
    public class StudentMiddleware
    {
        private readonly RequestDelegate _next;

        public StudentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, DatabaseContext dbContext)
        {
            if (context.Request.Path.Value.Substring(9).StartsWith("add")
                && context.Request.QueryString.HasValue)
            {
                var query = context.Request.QueryString.Value.Substring(1);

                var dict = new Dictionary<string, string>();
                foreach (var s in query.Split('&'))
                {
                    var strings = s.Split('=');
                    dict.Add(strings[0], strings[1]);
                }

                if (!dict.ContainsKey("name") ||
                    !dict.ContainsKey("score"))
                    throw new ValidationException("Name or score not provided");

                var student = new Student {Name = dict["name"], Score = int.Parse(dict["score"]) };

                if (dict.ContainsKey("teacherName"))
                {
                    student.Teachers = new[] {new Teacher
                    {
                        Name = dict["teacherName"],
                        Discipline = dict.ContainsKey("discipline") ? dict["discipline"] : null
                    }};
                }

                var entity = dbContext.Students.Add(student);

                await dbContext.SaveChangesAsync();

                await context.Response.WriteAsync(entity.Entity.Name);
            }
            else
            {
                    await _next(context);
            }
        }
    }
}
