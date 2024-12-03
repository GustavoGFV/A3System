using System.ComponentModel.DataAnnotations;

namespace A3System.Dbo.Dto.User
{
    public class CreateUserDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public string? RePassword { get; set; }

        public string? Role = "user";
    }
}

