using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //the init keyword means the feild cannot be modified
    public record CreateUserDto
    {
        [StringLength(11, ErrorMessage = "Invalid ID number"), Required]
        public string userIdNumber {get; init;} = string.Empty;
        [StringLength(50, ErrorMessage = "Email charecters must be atmost 50"), Required, EmailAddress]
        public string userEmail{get;init;} = string.Empty;
        [StringLength(50, ErrorMessage = "Name must atmost 50 charecters"), Required]
        public string name{get; init;} = null!;
        [StringLength(50, ErrorMessage = "surname must atmost 50 charecters"), Required]
        public string surname{get; init;} = string.Empty;
        [Required, StringLength(10, ErrorMessage = "A valid phone number is 10 charecters long"), MinLength(10, ErrorMessage = "A valid phone number is 10 charecters long")]
        public string phoneNumber{get; init;} = string.Empty;
        [Required, MinLength(8)]
        public string password{get; init;} = string.Empty;//not hashed yet
    }
}
