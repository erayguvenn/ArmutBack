using ArmutReborn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkListingController : ControllerBase
    {
        private readonly ArmutContext _context;

        public WorkListingController(ArmutContext context)
        {
            _context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkListing>>> GetWorkcategories()
        {
            return await _context.WorkListings.ToListAsync();
        }


        [HttpPost("worklist")]
        public async Task<ActionResult> WorkListingAdd([FromBody] WorkListingDTOcs value)
        {
            if (value == null) return BadRequest();

            WorkListing work = WorkListingDTOcs.WorkListingConverter(value); /* new WorkListing { CreatedAt = DateTime.Now,CategoryId=value.CategoryId,State=value.State,RuleFill=value.RuleFill }; */

            _context.WorkListings.Add(work);

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Ok" });
        }

    }
}
