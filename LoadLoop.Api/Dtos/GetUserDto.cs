using System.ComponentModel.DataAnnotations;

namespace LoadLoop.api.Dtos
{
    public record GetUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public int Id { get; set; }
        public int age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string UserRole { get; set; } = "U";
    }
}
