using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArmutReborn.Models;

namespace ArmutReborn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkcategoriesController : ControllerBase
    {
        private readonly ArmutContext _context;

        public WorkcategoriesController(ArmutContext context)
        {
            _context = context;
        }

        // GET: api/Workcategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workcategory>>> GetWorkcategories()
        {
            
            return await _context.Workcategories.ToListAsync();
        }

        // GET: api/Workcategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workcategory>> GetWorkcategory(uint id)
        {
            var workcategory = await _context.Workcategories.FindAsync(id);

            if (workcategory == null)
            {
                return NotFound();
            }

            return workcategory;
        }
       
    }
}
