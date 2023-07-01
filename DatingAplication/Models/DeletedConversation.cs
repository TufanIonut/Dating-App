using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class DeletedConversation
    {
        [Key]
        public int DeletedConversationId { get; set; }

        [Required]
        public int ConversationId { get; set; }

        [Required]
        public int DeletedByUserId { get; set; }

        public DateTime DeletedDateTime { get; set; }

        public Conversation Conversation { get; set; }
    }
}
