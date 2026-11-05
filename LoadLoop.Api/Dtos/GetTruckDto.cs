using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record GetTruckDto
    {
        public int TruckCapacity { get; set; }
        public int Id { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string? LicensePlate { get; set; } = string.Empty;
        public int UserId { get; set; } 
    }
}
