using ArmutReborn.DTO;
using ArmutReborn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerController : ControllerBase
    {
        private readonly ArmutContext _context;

        public WorkerController(ArmutContext context)
        {
            _context = context;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Worker>>> gerWorker(int id)
        {
            var worker = await _context.Workers.Include(worker => worker.User).Where(b => b.UserId == id).ToListAsync();
            if (worker == null) return NotFound();


            return Ok(worker.Select(worker => WorkerDTO.ToDTO(worker)).ToList());
        }
    }
}
