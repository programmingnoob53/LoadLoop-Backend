using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    //The user is not allowed to change the email(foreign key)
    //can only change two members
    public record UpdateTruckDto
    {
        [Required, Range(1000, int.MaxValue, ErrorMessage = "The weight of the truck must atlest be 1000kg or more.")]
        public int truckMaxWeight{get; set;}
        [Required]
        public string truckType{get; init;} = string.Empty;
    }
}
