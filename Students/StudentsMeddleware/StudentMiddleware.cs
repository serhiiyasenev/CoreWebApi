using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using FirstWebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace FirstWebApplication.StudentsMeddleware
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

                var dict = query.Split('&')
                    .Select(s => s.Split('='))
                    .ToDictionary(s => s[0], s => s[1]);

                if (!dict.ContainsKey("name") ||
                    !dict.ContainsKey("score"))
                    throw new ValidationException("Name or score not provided");

                var student = new Student {Name = dict["name"], Score = int.Parse(dict["score"]) };

                if (dict.ContainsKey("teacherName"))
                {
                    student.Teacher = new Teacher
                    {
                        Name = dict["teacherName"],
                        Discipline = dict.ContainsKey("discipline") ? dict["discipline"] : null
                    };
                }

                var entity = dbContext.Students.Add(student);

                await dbContext.SaveChangesAsync();

                await context.Response.WriteAsync(entity.Entity.Id.ToString());
            }
            else
            {
                    await _next(context);
            }
        }
    }
}
