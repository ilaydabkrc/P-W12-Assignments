using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurvivorProject.Context;
using SurvivorProject.Entities;

namespace SurvivorProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitorController : ControllerBase
    {
        private readonly SurvivorDbContext _context;
        public CompetitorController(SurvivorDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var competitors = await _context.Competitors.Include(c => c.Category).ToListAsync();
            return Ok(competitors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var competitor = await _context.Competitors.Include(c => c.Category).FirstOrDefaultAsync(c => c.Id == id);
            if (competitor == null) return NotFound();
            return Ok(competitor);
        }

        [HttpGet("categories/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var competitors = await _context.Competitors.Where(c => c.CategoryId == categoryId).ToListAsync();
            return Ok(competitors);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Competitor competitor)
        {
            competitor.CreatedDate = DateTime.UtcNow;
            _context.Competitors.Add(competitor);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = competitor.Id }, competitor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Competitor competitor)
        {
            var existing = await _context.Competitors.FindAsync(id);
            if (existing == null) return NotFound();
            existing.FirstName = competitor.FirstName;
            existing.LastName = competitor.LastName;
            existing.CategoryId = competitor.CategoryId;
            existing.ModifiedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var competitor = await _context.Competitors.FindAsync(id);
            if (competitor == null) return NotFound();
            competitor.IsDeleted = true;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}