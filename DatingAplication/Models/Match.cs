using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }

        public DateTime MatchDateTime { get; set; }
    }

}
