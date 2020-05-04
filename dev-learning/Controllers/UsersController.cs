using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dev_learning.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using dev_learning.Constants;

namespace dev_learning.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevLearningContext _context;

        public UsersController(DevLearningContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var isEmailValid = RegexUtilities.IsEmailValid(user.Email);
            if (isEmailValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetUser", new { id = user.Id }, user);
            } else
            {
                return ValidationProblem("Provided email is invalid");
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        // POST: api/Users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var users = await _context.Users.ToListAsync();
            var isUserValid = users.Exists(user => user.Email == authRequest.Email && user.Password == authRequest.Password);

            if (isUserValid)
            {
                var user = users.Find(user => user.Email == authRequest.Email);
                return Ok(GenerateAccessToken(user));
            }
            else
            {
                return Unauthorized("user is not valid");
            }
        }

        // GET: api/Users/current_user
        [HttpGet("current_user")]
        public ActionResult<TinyUserInfo> GetCurrentUser()
        {
            var currentUser = GetTinyUserInfo();
            if (currentUser != null)
            {
                return currentUser;
            }else
            {
                return Unauthorized();
            }
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimsNames.ID, user.Id.ToString()),
                new Claim(ClaimsNames.FullName, user.Firstname + " " + user.Lastname),
                new Claim(ClaimsNames.Email, user.Email),
                new Claim(ClaimsNames.Role, user.Role.ToString())
            };

            var bytes = Encoding.UTF8.GetBytes(JwtToken.EncryptionKey);
            var key = new SymmetricSecurityKey(bytes);
            var signingCredentials = new SigningCredentials(
              key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                JwtToken.Issuer,
                JwtToken.Audience,
                claims, notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private TinyUserInfo GetTinyUserInfo()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userId = identity.FindFirst(ClaimsNames.ID).Value;
                var userEmail = identity.FindFirst(ClaimsNames.Email).Value;
                var userRole = identity.FindFirst(ClaimsNames.Role).Value;
                var userFullName = identity.FindFirst(ClaimsNames.FullName).Value;
                return new TinyUserInfo(userFullName, userEmail, userRole, userId);
            }
            else
            {
                return null;
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
