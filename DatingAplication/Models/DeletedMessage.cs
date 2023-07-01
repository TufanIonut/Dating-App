using System.ComponentModel.DataAnnotations;

namespace DatingAplication.Models
{
    public class DeletedMessage
    {
        [Key]
        public int DeletedMessageId { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public int DeletedByUserId { get; set; }

        public DateTime DeletedDateTime { get; set; }

        public Message Message { get; set; }
    }

}
