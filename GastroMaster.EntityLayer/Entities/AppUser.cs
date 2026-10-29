

using Microsoft.AspNetCore.Identity;

namespace GastroMaster.EntityLayer.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name  { get; set; }
        public string Surname { get; set; }
    }
}
