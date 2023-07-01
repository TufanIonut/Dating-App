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
    public class LikeController : ControllerBase
    {
        private readonly ILogger<LikeController> _logger;
        private readonly DatingAppDb _dbContext;

        public LikeController(ILogger<LikeController> logger, DatingAppDb dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLike([FromBody] CreateLikeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var liker = await _dbContext.Users.FindAsync(request.LikerId);
            if (liker == null)
            {
                return NotFound($"Utilizatorul cu ID-ul {request.LikerId} nu există.");
            }

            var likedUser = await _dbContext.Users.FindAsync(request.LikedUserId);
            if (likedUser == null)
            {
                return NotFound($"Utilizatorul cu ID-ul {request.LikedUserId} nu există.");
            }

            var existingLike = await _dbContext.Likes.FindAsync(request.LikerId, request.LikedUserId);
            if (existingLike != null)
            {
                return BadRequest("Like-ul deja există.");
            }
            var like = new Like
            {
                SenderId = request.LikerId,
                ReceiverId = request.LikedUserId,
     
            };

            try
            { 
                _dbContext.Likes.Add(like);
                await _dbContext.SaveChangesAsync();

 
                return Ok(like);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la crearea like-ului");
                return StatusCode(500, "A apărut o eroare la crearea like-ului.");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLike(int likerId, int likedUserId)
        {

            var like = await _dbContext.Likes.FindAsync(likerId, likedUserId);
            if (like == null)
            {
                return NotFound("Like-ul nu a fost găsit.");
            }

            try
            {

                _dbContext.Likes.Remove(like);
                await _dbContext.SaveChangesAsync();

                var response = new
                {
                    Message = "Like-ul a fost șters cu succes!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la ștergerea like-ului");
                return StatusCode(500, "A apărut o eroare la ștergerea like-ului.");
            }
        }
    }

    public class CreateLikeRequest
    {
        [Required]
        public int LikerId { get; set; }

        [Required]
        public int LikedUserId { get; set; }
    }
}
