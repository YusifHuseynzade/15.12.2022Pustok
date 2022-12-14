using Microsoft.AspNetCore.Identity;

namespace PustokDb2022.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
