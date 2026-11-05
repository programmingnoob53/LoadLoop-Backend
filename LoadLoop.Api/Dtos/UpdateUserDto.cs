using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public class UpdateUserDto
    {

        [EmailAddress, StringLength(50, ErrorMessage = "Email too long")]
        public string? UserEmail { get; init; } = string.Empty;

        [MinLength(8, ErrorMessage = "Password must be atlease 8 charecters")]
        public string? UserPassword { get; init; } = string.Empty;

        [Range(18, 55, ErrorMessage = "Age must be between 18 and 55")]
        public int? UserAge { get; init;  }
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

    }
}
