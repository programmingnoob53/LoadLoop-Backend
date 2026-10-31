using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //The user is not allowed to change the email(foreign key)
    public class UpdateTruckDto
    {
        [Required]
        public int Id { get; init; }
        [StringLength(500)]
        public string ImageURL {  get; init; } = string.Empty;
        [StringLength(10)]
        public string LicensePlate { get; init; } = string.Empty;
    }
}
