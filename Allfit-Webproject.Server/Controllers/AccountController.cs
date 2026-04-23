using Microsoft.AspNetCore.Mvc;
using Allfit_Webproject.Server.Data;
using Allfit_Webproject.Server.Services;
using Allfit_Webproject.Server.Dtos;

namespace Allfit_Webproject.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ITokenService _tokenService;

        public AccountController(ApplicationDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }
        //alle dto's naar een andere map verplaatsen.

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var user = _db.Lidmaatschappen.SingleOrDefault(u => u.email == dto.Email);
            if (user == null) return Unauthorized("Fout e-mail wachtwoord combinatie.");

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.wacthwoord))
                return Unauthorized("Fout e-mail wachtwoord combinatie.");

            var token = _tokenService.CreateToken(user.id.ToString(), user.email);

            // token and basic user info so the client can display the account
            return Ok(new
            {
                token,
                user = new
                {
                    name = user.naam,
                    email = user.email,
                }
            });
        }
    }
}
