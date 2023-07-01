using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public int Age { get; set; }
        [Required]
        public string? Gender { get; set; }
    }
}
