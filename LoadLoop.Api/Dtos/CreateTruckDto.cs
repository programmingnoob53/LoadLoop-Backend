using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record CreateTruckDto
    {
        [Required, StringLength(500, ErrorMessage = "URL too long")]
        public string TruckImage { get; init; } = string.Empty;
        [Required, StringLength(10, ErrorMessage = "License Plate too long")]
        public string LicensePlate { get; init; } = string.Empty;
        [Required, StringLength(50, ErrorMessage = "Email too long"), EmailAddress]
        public string UserEmail {  get; init; } = string.Empty;
    }
}
