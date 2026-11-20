using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record GetTruckDto
    {
        public int truckID{get; set;}
        public string truckType{get; set;} = string.Empty;
        public int truckMaxweight{get; set;}
        public string truckLicensePlate{get; set;} = string.Empty;
        public string truckImage{get; set;} = string.Empty;
    }
}
