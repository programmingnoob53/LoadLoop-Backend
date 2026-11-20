using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record CreateDriverDto
    {
        [StringLength(11, ErrorMessage = "Invalid ID number"), Required]
        public string userIdNumber {get; init;} = string.Empty;
        [StringLength(50, ErrorMessage = "Email charecters must be atmost 50"), Required, EmailAddress]
        public string userEmail{get;init;} = string.Empty;
        [StringLength(50, ErrorMessage = "Name must atmost 50 charecters"), Required]
        public string name{get; init;} = null!;
        [StringLength(50, ErrorMessage = "surname must atmost 50 charecters"), Required]
        public string surname{get; init;} = string.Empty;
        [Required,Phone, StringLength(10, ErrorMessage = "A valid phone number is 10 charecters long"), MinLength(10, ErrorMessage = "A valid phone number is 10 charecters long")]
        public string phoneNumber{get; init;} = string.Empty;
        [Required, MinLength(8)]
        public string password{get; init;} = string.Empty;
        //for the driver table
        [Required, Range(9000, int.MaxValue, ErrorMessage = "The salary must be atleast R9000")]
        public decimal money{get; init;}
        [Required]
        public bool isAvailable{get; init;}
    }
}