using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public class PatchTruckDto
    {
        [Range(1000, int.MaxValue, ErrorMessage = "The weight of the truck must atlest be 1000kg or more.")]
        public int? truckMaxWeight { get; set; }
        [Required]
        public string? truckType { get; init; } = string.Empty;
    }
}
