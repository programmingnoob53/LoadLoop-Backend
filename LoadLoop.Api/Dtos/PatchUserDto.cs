using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record PatchUserDto
    {
        [EmailAddress, MaxLength(50, ErrorMessage = "The maximum length of an email is 50 charecters.")]
        public string? newUserEmail { get; init; }
        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 charecters.")]
        public string? newUserName { get; init; }
        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 charecters.")]
        public string? newUserSurname {  get; init; }
        [MinLength(8)]
        public string? newPassword { get; init; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "The number must be exactly 10 digits")]
        public string? newPhoneNumber { get; init; }
    }
}
