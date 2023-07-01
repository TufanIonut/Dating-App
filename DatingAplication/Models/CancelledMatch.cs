using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class CancelledMatch
    {
        [Key]
        public int CancelledMatchId { get; set; }

        [Required]
        public int MatchId { get; set; }

        [Required]
        public int CancelledByUserId { get; set; }

        public DateTime CancelledDateTime { get; set; }

        public Match Match { get; set; }
    }
}
