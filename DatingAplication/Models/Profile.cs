using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; set; }

        [Required]
        public int UserId { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string? Bio { get; set; }

        public string? Location { get; set; }

        public string? Interests { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string? ProfilePictureURL { get; set; }

        public User User { get; set; }
    }

}
