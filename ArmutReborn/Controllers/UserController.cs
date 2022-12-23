using ArmutReborn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArmutReborn.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ArmutContext _context;

        public UserController(ArmutContext context)
        {
            _context = context;
        }

        // GET: api/<UserController>
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetMe()
        {
            uint userId = uint.Parse(User.FindFirst("userId").Value);
            var user = await _context.Users.Where(user => user.Id == userId).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>>Get(int id)
        {
            var userWithSameId = await  _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();

            if(userWithSameId == null) return NotFound();
            return userWithSameId;
        }
     
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
                
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
