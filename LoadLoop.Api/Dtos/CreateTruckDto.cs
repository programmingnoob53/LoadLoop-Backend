using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record CreateTruckDto
    {
        [Required]
        public int driverID{get; init;} //who this truck belongs to
        [Required, StringLength(12, ErrorMessage = "The truck type must be atmost 12 charecters")]
        public string truckType{get; init;} = string.Empty;
        [Required, Range(1000, int.MaxValue, ErrorMessage = "The capacity must be atleast 1000kg")]
        public int truckMaxWeight{get; init;}
        [Required]
        public string truckLicensePlate{get; init;} = string.Empty;
        [Required]
        public string truckImage{get; init;} = string.Empty;
    }
}
