using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }

        public bool IsMatch { get; set; }
    }

}
