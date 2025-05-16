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
    public class MachineJobController : ControllerBase
    {
        private readonly S31JdfMachineHandlerContext _context;

        public MachineJobController(S31JdfMachineHandlerContext context)
        {
            _context = context;
        }

        // GET: api/MachineJob
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineJob>>> GetMachineJobs()
        {
            return await _context.MachineJobs.ToListAsync();
        }

        // GET: api/MachineJob/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MachineJob>> GetMachineJob(int id)
        {
            var machineJob = await _context.MachineJobs.FindAsync(id);

            if (machineJob == null)
            {
                return NotFound();
            }

            return machineJob;
        }

        // PUT: api/MachineJob/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachineJob(int id, MachineJob machineJob)
        {
            if (id != machineJob.Id)
            {
                return BadRequest();
            }

            _context.Entry(machineJob).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MachineJobExists(id))
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

        // POST: api/MachineJob
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MachineJob>> PostMachineJob(MachineJob machineJob)
        {
            _context.MachineJobs.Add(machineJob);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachineJob", new { id = machineJob.Id }, machineJob);
        }

        // DELETE: api/MachineJob/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachineJob(int id)
        {
            var machineJob = await _context.MachineJobs.FindAsync(id);
            if (machineJob == null)
            {
                return NotFound();
            }

            _context.MachineJobs.Remove(machineJob);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachineJobExists(int id)
        {
            return _context.MachineJobs.Any(e => e.Id == id);
        }
    }
}
