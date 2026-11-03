using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //the init keyword means the feild cannot be modified
    public record CreateUserDto
    {
        [Required, Range(18, 55, ErrorMessage = "The user age must be between 18 and 55(inclusive)"),]
        public int UserAge { get; init;  }
        [Required, EmailAddress, StringLength(50, ErrorMessage = "Email too long")]
        public string UserEmail { get; init; } = string.Empty;
        [Required, MinLength(8, ErrorMessage = "Password must be atleast 8 characters")]
        public string Password { get; init; } = string.Empty;
        [RegularExpression("^[UT]$", ErrorMessage = "Allowed roles: U: user, T: transport provider")]//admin registered manually
        public char UserRole { get; init; }
        
    }
}
