using System.ComponentModel.DataAnnotations;

namespace EFCoreOnDeleteTest.DTO
{
    public class AuthenticateModelDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
