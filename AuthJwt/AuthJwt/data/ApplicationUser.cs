using Microsoft.AspNetCore.Identity;
namespace AuthJwt.data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
