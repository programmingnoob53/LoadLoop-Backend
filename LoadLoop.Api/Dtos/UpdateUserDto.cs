using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //a user cannot change their role
    //a user cannot change their id number
    public class UpdateUserDto
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
    }
}
