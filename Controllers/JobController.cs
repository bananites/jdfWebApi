using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataPollingApi.Models;

namespace DataPollingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly S31JdfMachineHandlerContext _context;

        public JobController(S31JdfMachineHandlerContext context)
        {
            _context = context;
        }

        // GET: api/Job
        [HttpGet]
        public ActionResult<IEnumerable<JobDTO>> GetJobs()
        {
            var jobs = _context.Jobs
            .Select(j => new JobDTO
            {
                Id = j.Id,
                XmlPath = j.XmlPath
            })
            .ToList();


            return jobs;
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public  ActionResult<JobDTO> GetJob(int id)
        {
            var job = _context.Jobs.Find(id);

            if (job == null)
            {
                return NotFound();
            }

            var map = new JobDTO
            {
                Id = job.Id,
                XmlPath = job.XmlPath
            };

            return map;
        }

        // PUT: api/Job/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
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

        // POST: api/Job
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
