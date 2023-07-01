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
    public class MatchController : ControllerBase
    {
        private readonly ILogger<MatchController> _logger;
        private readonly DatingAppDb _db;

        public MatchController(ILogger<MatchController> logger, DatingAppDb db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user1 = await _db.Users.FindAsync(request.User1Id);
            if (user1 == null)
            {
                return NotFound($"Utilizatorul cu ID-ul {request.User1Id} nu există.");
            }

            var user2 = await _db.Users.FindAsync(request.User2Id);
            if (user2 == null)
            {
                return NotFound($"Utilizatorul cu ID-ul {request.User2Id} nu există.");
            }

            var match = new Match
            {
                User1Id = request.User1Id,
                User2Id = request.User2Id,
                MatchDateTime = DateTime.UtcNow
            };

            try
            {
                _db.Matches.Add(match);
                await _db.SaveChangesAsync();

                return Ok(match);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la crearea match-ului");
                return StatusCode(500, "A apărut o eroare la crearea match-ului.");
            }
        }

        [HttpGet("{matchId}")]
        public async Task<IActionResult> GetMatch(int matchId)
        {
            var match = await _db.Matches.FindAsync(matchId);
            if (match == null)
            {
                return NotFound("Match-ul nu a fost găsit.");
            }

            return Ok(match);
        }
    }

    public class CreateMatchRequest
    {
        [Required]
        public int User1Id { get; set; }

        [Required]
        public int User2Id { get; set; }
    }
}
