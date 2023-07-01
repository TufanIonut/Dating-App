using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class DeletedProfile
    {
        [Key]
        public int DeletedProfileId { get; set; }

        [Required]
        public int ProfileId { get; set; }

        [Required]
        public int DeletedByUserId { get; set; }

        public DateTime DeletedDateTime { get; set; }

        public Profile Profile { get; set; }
    }
}
