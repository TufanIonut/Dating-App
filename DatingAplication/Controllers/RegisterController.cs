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
    public class RegisterController : ControllerBase
    {
        private readonly ILogger<RegisterController> _logger;
        private readonly DatingAppDb _dbContext;

        public RegisterController(ILogger<RegisterController> logger, DatingAppDb dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _dbContext.Users.AnyAsync(u => u.Name == request.Name))
            {
                return BadRequest("Numele de utilizator este deja înregistrat.");
            }

            var user = new User
            {
                Name = request.Name,
                PasswordHash = request.PasswordHash,
                Age = request.Age,
                Gender = request.Gender
            };

            try
            {
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            
                var response = new
                {
                    Message = "Utilizatorul a fost înregistrat cu succes!"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la înregistrarea utilizatorului");
                return StatusCode(500, "A apărut o eroare la înregistrarea utilizatorului.");
            }
        }
    }

    public class RegisterRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
