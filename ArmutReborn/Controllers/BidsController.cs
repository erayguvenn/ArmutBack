using ArmutReborn.DTO;
using ArmutReborn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {

        private readonly ArmutContext _context;

        public BidsController(ArmutContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidsAll()
        {
            return await _context.Bids.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBids(int id)
        {
            var bid = await _context.Bids.Include(bid => bid.Worker).Include(bid => bid.Worker.User).Where(b => b.WorklistingId == id).ToListAsync();
            if (bid == null) return NotFound();

            
            return Ok(bid.Select(bid => BidAllValueDTO.ToDTO(bid)).ToList());
        }

        [HttpPost("bids")]
        public async Task<ActionResult> BidAdd([FromBody] BidDTO value)
        {
            if (value == null) return BadRequest();

            Bid bid = BidDTO.BidConverter(value);

            _context.Bids.Add(bid);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Ok" });
        }

    }


}
