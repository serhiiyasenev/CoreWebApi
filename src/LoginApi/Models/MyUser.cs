using Microsoft.AspNetCore.Identity;

namespace LoginApi.Models
{
    public class MyUser : IdentityUser
    {
        public string LastName { get; set; }
    }
}
