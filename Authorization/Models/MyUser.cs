using Microsoft.AspNetCore.Identity;

namespace CoreWebApp.Authorization.Models
{
    public class MyUser : IdentityUser
    {
        public string LastName;
    }
}
