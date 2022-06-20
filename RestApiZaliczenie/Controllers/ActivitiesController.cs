using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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


        [HttpGet("random")]
        public async Task<ActionResult<Activity>> GetRandomActivity(int id)
        {
            int max = _context.Activities.Max(p => p.Id);
            int min = _context.Activities.Min(p => p.Id);

            Random r = new Random();

            id = r.Next(min, max +1); //for ints

            var activity = await _context.Activities.FindAsync(id);

            if (activity == null)
            {
                id = r.Next(min, max + 1); //for ints

                activity = await _context.Activities.FindAsync(id);
            }

            return activity;
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
                if(activity.Name != "" && !Regex.IsMatch(activity.Name, @"^\d+$"))
                {
                    _context.Activities.Add(activity);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction(nameof(GetActivity), new { id = activity.Id }, activity);
                }
                return BadRequest();
                
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
