using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [Required]
        public int SenderId { get; set; }

        public string Content { get; set; }
        public DateTime SentDateTime { get; set; }

        public Conversation Conversation { get; set; }
    }

}
