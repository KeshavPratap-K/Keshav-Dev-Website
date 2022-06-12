using System.ComponentModel.DataAnnotations;

namespace Keshav_Dev.Model
{
    public class RegisterUser
    {
        public string email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
