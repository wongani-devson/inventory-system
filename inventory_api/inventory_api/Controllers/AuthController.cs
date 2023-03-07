using inventory_api.Data;
using inventory_api.Models;
using inventory_api.Services.AuthService;

using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace inventory_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;
        private readonly ApplicationDbContext _context;

        public AuthController(ILogger<AuthController> logger,IConfiguration configuration, IAuthService authService, ApplicationDbContext context) 
        {
            _configuration = configuration;
            _logger = logger;
            _authService= authService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> RegisterUser(UserRegister req)
        {
            var user = await _authService.RegisterUser(req);

            if(user == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> LoginUser(UserRegister req)
        {
            var user = await _authService.LoginUser(req);

            if (user == null)
            {
                return BadRequest("User not found.");
            }
            if(!VerifyPasswordHarsh(req.Password, user.PasswordHash, user.PasswordSalt)) 
            {
                return BadRequest("Email or password is incorrect");
            }
            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private bool VerifyPasswordHarsh(string password, byte[] passwordHash, byte[] passwordSalt) 
        { 
            using(var hmac = new HMACSHA512(passwordSalt)) 
            {
                var computedHarsh = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHarsh.SequenceEqual(passwordHash);
            }
        }
    }
}
