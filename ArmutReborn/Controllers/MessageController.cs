using ArmutReborn.DTO;
using ArmutReborn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ArmutContext _context;

        public MessageController(ArmutContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        [HttpPost("messages")]
        public async Task<ActionResult> AddMessage([FromBody] MessageDTO value)
        {
            if (value == null) return BadRequest();

            Message message = MessageDTO.MessageConverter(value);

            _context.Messages.Add(message);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Ok" });
        }
    }
}
