using magooshAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace magooshAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                string hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword;
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Signup(User user)
        //{
        //    user.Password = HashPassword(user.Password);
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return Ok("User registered successfully");
        //}

        //public async Task<User> Authenticate(string username, string password)
        //{
        //    var user = await _context.Users.FirstOrDefaultAsync(u=> u.UserName == username);

        //    if (user == null || HashPassword(password)!=user.Password) { return null; }

        //    return user;
        //}
    }
}
