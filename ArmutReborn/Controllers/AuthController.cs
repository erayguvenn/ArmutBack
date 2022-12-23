using ArmutReborn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ArmutContext _context;

        public AuthController(ArmutContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserPostDTO value)
        {
            if (value == null) return BadRequest();

            //Kayıt olurken gönderilen email'in kullanımda olup olmadığı kontrolü
            User userWithSameEmail = await _context.Users.Where(user => user.Email == value.Email).FirstOrDefaultAsync();
            if(userWithSameEmail != null) return BadRequest(new {Message="Bu email zaten kullanılıyor"});

            User newUser = new User { CreatedAt = DateTime.Now, Email = value.Email, Name = value.Name, Surname = value.Surname, PhoneNumber = value.PhoneNumber, Password = value.Password };

            if (value.UserType =="worker" )
            {
                Worker worker = new Worker { Adress = value.Adress };
                worker.User = newUser;
                _context.Workers.Add(worker);

            }
            _context.Users.Add(newUser);



            await _context.SaveChangesAsync();

            return Ok(new { Message = "Ok" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO value)
        {
            if (value == null) return NotFound();

            //Girilen email ve şifre ile tutarlı bir kullanıcı sorgusu
            User userToLogIn = await _context.Users.Where(user => user.Email == value.Email && user.Password == value.Password).FirstOrDefaultAsync();
            if (userToLogIn == null) return BadRequest();


            var claims = new List<Claim>
            {
                new Claim("userId", userToLogIn.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            //return Ok(new { Message = "Ok"});
            return Ok(userToLogIn.Id);
        }

    }
}
