using Microsoft.AspNetCore.Identity;

namespace FirstWebApplication.Authorization.Models
{
    public class MyUser : IdentityUser
    {
        public string LastName;
    }
}
