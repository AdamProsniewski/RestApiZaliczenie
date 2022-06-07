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
    public class ActivitiesController : ControllerBase
    {
        private readonly DataContext _context;

        public ActivitiesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Activities
        [HttpGet(Name ="GetActivities")]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            return await _context.Activities.ToListAsync();
        }

        // GET: api/Activities/5
        [HttpGet("{id}", Name ="GetActivity")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // GET: api/Activities/last
        [HttpGet("last", Name = "GetLastActivity")]
        public async Task<ActionResult<Activity>> GetLastActivity()
        {
            int max = _context.Activities.Max(p => p.Id);
            var activity = await _context.Activities.FindAsync(max);

            if (activity == null)
            {
                return NotFound();
            }

            return activity;
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            try
            {
                _context.Entry(activity).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                    return NoContent();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return BadRequest(ex.Message);
            }

        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            try 
            {
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
            }
            catch(Exception ex) 
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return BadRequest(ex.Message);
            }
            
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {

            try
            {
                var activity = await _context.Activities.FindAsync(id);
                if (activity == null)
                {
                    return NotFound();
                }

                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return BadRequest(ex.Message);
            }

        }


        // DELETE: api/Activities/5
        [HttpDelete("last")]
        public async Task<IActionResult> DeleteLastActivity()
        {

            try
            {
                int max = _context.Activities.Max(p => p.Id);
                var activity = await _context.Activities.FindAsync(max);
                if (activity == null)
                {
                    return NotFound();
                }

                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }

                return BadRequest(ex.Message);
            }

        }


        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.Id == id);
        }
    }
}
