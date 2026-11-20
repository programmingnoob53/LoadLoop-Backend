using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record GetUserDto
    {
        public int userID{get; set;}
        public string userName{get; set;} = string.Empty;
        public string userEmail{get;set;} = string.Empty;
        public string userSurname{get;set;} = string.Empty;
        public string phoneNumber{get; set;} = string.Empty;
    }
}
