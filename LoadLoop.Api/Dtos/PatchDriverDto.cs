using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record PatchDriverDto
    {
        [EmailAddress, MaxLength(50, ErrorMessage = "The maximum length of an email is 50 charecters.")]
        public string? newUserEmail { get; init; }
        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 charecters.")]
        public string? newUserName { get; init; }
        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 charecters.")]
        public string? newUserSurname { get; init; }
        [MinLength(8)]
        public string? newPassword { get; init; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The number must be exactly 10 digits."), Phone]
        public string? newPhoneNumber { get; init; }
        [Range(5000, int.MaxValue, ErrorMessage = "The salary must be atleast R5000.")]
        public decimal? newSalary { get; init; }
        public bool? isAvailable {  get; init; }
    }
}
