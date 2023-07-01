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
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly DatingAppDb _dbContext;

        public ProfileController(ILogger<ProfileController> logger, DatingAppDb dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] UpdateProfileRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("Utilizatorul nu a fost găsit.");
            }

            user.Bio = request.Bio;
            user.Location = request.Location;
            user.Interests = request.Interests;
            user.ProfilePictureURL = request.ProfilePictureURL;

            try
            {

                await _dbContext.SaveChangesAsync();

   
                var response = new
                {
                    Message = "Informațiile profilului au fost actualizate cu succes!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la actualizarea informațiilor profilului");
                return StatusCode(500, "A apărut o eroare la actualizarea informațiilor profilului.");
            }
        }
    }

    public class UpdateProfileRequest
    {
        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string Bio { get; set; }

        public string Location { get; set; }

        public string Interests { get; set; }

        [StringLength(int.MaxValue, MinimumLength = 4)]
        public string ProfilePictureURL { get; set; }
    }
}
