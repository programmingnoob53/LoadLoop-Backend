using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record GetDriverDto
    {
        public int driverID{get;set;}
        public string userName{get; set;} = string.Empty;
        public string userEmail{get;set;} = string.Empty;
        public string userSurname{get;set;} = string.Empty;
        public string phoneNumber{get; set;} = string.Empty;
        public Decimal money{get; set;}
        public bool isAvailable{get; set;}
    }
}