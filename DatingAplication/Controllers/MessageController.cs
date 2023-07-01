using DatingAplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DatingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly DatingAppDb _dbContext;

        public MessageController(ILogger<MessageController> logger, DatingAppDb dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var conversation = await _dbContext.Conversations.FindAsync(request.ConversationId);
            if (conversation == null)
            {
                return NotFound($"Conversația cu ID-ul {request.ConversationId} nu există.");
            }

            var sender = await _dbContext.Users.FindAsync(request.SenderId);
            if (sender == null)
            {
                return NotFound($"Utilizatorul cu ID-ul {request.SenderId} nu există.");
            }
          var message = new Message
            {
                ConversationId = request.ConversationId,
                SenderId = request.SenderId,
                Content = request.Content,
                SentDateTime = DateTime.UtcNow
            };

            try
            {
                _dbContext.Messages.Add(message);
                await _dbContext.SaveChangesAsync();


                return Ok(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la trimiterea mesajului");
                return StatusCode(500, "A apărut o eroare la trimiterea mesajului.");
            }
        }

        [HttpGet("{messageId}")]
        public async Task<IActionResult> GetMessage(int messageId)
        {
  
            var message = await _dbContext.Messages.FindAsync(messageId);
            if (message == null)
            {
                return NotFound("Mesajul nu a fost găsit.");
            }

            return Ok(message);
        }
    }

    public class SendMessageRequest
    {
        [Required]
        public int ConversationId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
