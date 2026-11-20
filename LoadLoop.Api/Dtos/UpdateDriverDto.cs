using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record UpadateDriverDto
    {
        [Required, StringLength(50, ErrorMessage = "The useremail must be atmost 50 charecters."), EmailAddress]
        public string userEmail{get; init;} = string.Empty;

        [Required, StringLength(50, ErrorMessage = "The name must be atmost 50 charecters.")]
        public string userName{get; init;} = string.Empty;

        [Required, StringLength(50, ErrorMessage = "The surname Length must be atmost 50 charecters.")]
        public string userSurname{get; init;} = string.Empty;

        [Required, StringLength(10), Phone]
        public string phoneNumber{get; init;} = string.Empty;
        [Required, MinLength(8)]
        public string newPassword{get; init;} = string.Empty;
        [Required, Range(9000, int.MaxValue, ErrorMessage = "The new Salary must be atleast R9000.")]
        public decimal money{get; set;}
        [Required]
        public bool isAvailable{get; set;}
    }
}