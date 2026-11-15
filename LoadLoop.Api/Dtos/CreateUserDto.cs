using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //the init keyword means the feild cannot be modified
    public record CreateUserDto
    {
        [StringLength(11, ErrorMessage = "Invalid ID number"), Required]
        string userIdNumber {get; init;} = null!;
        [StringLength(50, ErrorMessage = "Email charecters must be atmost 50"), Required, EmailAddress]
        string userEmail{get;init;} = null!;
        [StringLength(50, ErrorMessage = "Name must atmost 50 charecters"), Required]
        string name{get; init;} = null!;
        [StringLength(50, ErrorMessage = "surname must atmost 50 charecters"), Required]
        string surname{get; init;} = null!;
        [Required, StringLength(10, ErrorMessage = "A valid phone number is 10 charecters long"), MinLength(10, ErrorMessage = "A valid phone number is 10 charecters long")]
        string phoneNumber{get; init;} = null!;
        [Required, MinLength(8)]
        string password{get; init;} = null!;//not hashed yet
        
        [Required(ErrorMessage = "Account type is required.")]
        [StringLength(1, ErrorMessage = "Account type must be one character.")]
        [RegularExpression(@"^[UT]$",
        ErrorMessage = "Account type must be U (User) or T (Transport Provider).")]
        string userRole {get; init;} = null!;

    }
}
