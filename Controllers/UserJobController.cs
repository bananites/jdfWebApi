using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataPollingApi.Models;

namespace DataPollingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserJobController : ControllerBase
    {
        private readonly S31JdfMachineHandlerContext _context;

        public UserJobController(S31JdfMachineHandlerContext context)
        {
            _context = context;
        }

        // GET: api/UserJob
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserJob>>> GetUserJobs()
        {
            return await _context.UserJobs.ToListAsync();
        }

        // GET: api/UserJob/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserJob>> GetUserJob(int id)
        {
            var userJob = await _context.UserJobs.FindAsync(id);

            if (userJob == null)
            {
                return NotFound();
            }

            return userJob;
        }

        // PUT: api/UserJob/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserJob(int id, UserJob userJob)
        {
            if (id != userJob.Id)
            {
                return BadRequest();
            }

            _context.Entry(userJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserJobExists(id))
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

        // POST: api/UserJob
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserJob>> PostUserJob(UserJob userJob)
        {
            _context.UserJobs.Add(userJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserJob", new { id = userJob.Id }, userJob);
        }

        // DELETE: api/UserJob/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserJob(int id)
        {
            var userJob = await _context.UserJobs.FindAsync(id);
            if (userJob == null)
            {
                return NotFound();
            }

            _context.UserJobs.Remove(userJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserJobExists(int id)
        {
            return _context.UserJobs.Any(e => e.Id == id);
        }
    }
}
