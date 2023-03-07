using inventory_api.Data;
using inventory_api.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Serialization;
using System.Security.Claims;

using System.Security.Cryptography;

namespace inventory_api.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterUser(UserRegister req)
        {
            var user = new User();

            CreatePasswordHash(req.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.UserName = req.Username;
            user.Email = req.Email;
            user.FirstName = req.FirstName;
            user.LastName = req.LastName;
            user.PhoneNumber = req.PhoneNumber;
            user.CreatedAt = DateTime.UtcNow;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Entry(user).State = EntityState.Added;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> LoginUser(LoginDTO req)
        {
            var user = await _context.Users.FindAsync(req.Email);

            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }

            return user;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [Serializable]
        internal class NotFoundException : Exception
        {
            public NotFoundException()
            {
            }

            public NotFoundException(string? message) : base(message)
            {
            }

            public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
