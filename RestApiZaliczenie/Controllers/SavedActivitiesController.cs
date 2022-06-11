using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApiZaliczenie;
using RestApiZaliczenie.Data;

namespace RestApiZaliczenie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedActivitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public SavedActivitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SavedActivities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedActivities>>> GetSavedActivities()
        {
            return await _context.SavedActivities.ToListAsync();
        }

        // GET: api/SavedActivities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SavedActivities>> GetSavedActivities(int id)
        {
            var savedActivities = await _context.SavedActivities.FindAsync(id);

            if (savedActivities == null)
            {
                return NotFound();
            }

            return savedActivities;
        }

        // PUT: api/SavedActivities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSavedActivities(int id, SavedActivities savedActivities)
        {
            if (id != savedActivities.Id)
            {
                return BadRequest();
            }

            _context.Entry(savedActivities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SavedActivitiesExists(id))
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

        // POST: api/SavedActivities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SavedActivities>> PostSavedActivities(SavedActivities savedActivities)
        {
            _context.SavedActivities.Add(savedActivities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSavedActivities", new { id = savedActivities.Id }, savedActivities);
        }

        // DELETE: api/SavedActivities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavedActivities(int id)
        {
            var savedActivities = await _context.SavedActivities.FindAsync(id);
            if (savedActivities == null)
            {
                return NotFound();
            }

            _context.SavedActivities.Remove(savedActivities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SavedActivitiesExists(int id)
        {
            return _context.SavedActivities.Any(e => e.Id == id);
        }
    }
}
