using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class Conversation
    {
        [Key]
        public int ConversationId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        public ICollection<Message> Messages { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }

}
