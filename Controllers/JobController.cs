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

        // GET: api/job
        // DONE
        [HttpGet]
        public ActionResult<IEnumerable<JobDTO>> GetJobs()
        {
            var jobs = _context.Jobs
            .Include(j => j.UserJobs)
            .ThenInclude(uj => uj.User)
            .Include(j => j.MachineJobs)
            .ThenInclude(mj => mj.Machine)
            .Select(j => new JobDTO
            {
                Id = j.Id,
                Name = j.Name,
                XmlPath = j.XmlPath,
                Status = j.Status,

                MachineId = j.MachineJobs
                .Select(mj => mj.MachineId)
                .FirstOrDefault(),

                Users = j.UserJobs
                .Select(uj => new UserDTO
                {
                    Id = uj.User.Id,
                    Username = uj.User.Username
                }).ToList()

            })
            .ToList();

            return jobs;
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public ActionResult<JobDTO> GetJob(int id)
        {
            var job = _context.Jobs
            .Include(j => j.UserJobs)
            .ThenInclude(uj => uj.User)
            .Include(j => j.MachineJobs)
            .ThenInclude(mj => mj.Machine)
            .Select(j => new JobDTO
            {
                Id = j.Id,
                Name = j.Name,
                XmlPath = j.XmlPath,
                Status = j.Status,

                MachineId = j.MachineJobs
                .Select(mj => mj.MachineId)
                .FirstOrDefault(),

                Users = j.UserJobs
                .Select(uj => new UserDTO
                {
                    Id = uj.User.Id,
                    Username = uj.User.Username
                }).ToList()
            })
            .FirstOrDefault(j => j.Id == 5);

            if (job == null)
            {
                return NotFound();
            }

            var map = new JobDTO
            {
                Id = job.Id,
                XmlPath = job.XmlPath

            };

            return job;
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
