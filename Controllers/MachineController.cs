using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataPollingApi.Models;

// TODO Maybe I want to have different routes for Machines and Machines with Jobs
namespace DataPollingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MachineController : ControllerBase
    {
        private readonly S31JdfMachineHandlerContext _context;

        public MachineController(S31JdfMachineHandlerContext context)
        {
            _context = context;
        }

        // GET: api/Machine
        [HttpGet]
        public ActionResult<IEnumerable<MachineDTO>> GetMachines()
        {
            var machines = _context.Machines
            .Include(m => m.MachineJobs)
            .ThenInclude(mj => mj.Job)
            .Select(m => new MachineDTO
            {
                Id = m.Id,
                Type = m.Type,
                YearBuilt = m.YearBuilt,
                MachineJobs = m.MachineJobs
                .Where(mj => mj.Job != null)
                .Select(mj => new JobDTO
                {
                    Id = mj.Job.Id,
                    Name = mj.Job.Name,
                    Status = mj.Job.Status,
                    XmlPath = mj.Job.XmlPath,

                }).ToList()
            })
            .ToList();

            return machines;
        }

        // GET: api/Machine/5
        [HttpGet("{id}")]
        public ActionResult<MachineDTO> GetMachine(int id)
        {
            var machine = _context.Machines
            .Include(m => m.MachineJobs)
            .ThenInclude(mj => mj.Job)
            .FirstOrDefault(m => m.Id == id);

            if (machine == null)
            {
                return NotFound();
            }

            var map = new MachineDTO
            {
                Id = machine.Id,
                Type = machine.Type,
                YearBuilt = machine.YearBuilt,
                MachineJobs = machine.MachineJobs
                .Where(mj => mj.Job != null)
                .Select(mj => new JobDTO
                {
                    Id = mj.Job.Id,
                    Name = mj.Job.Name,
                    Status = mj.Job.Status,
                    XmlPath = mj.Job.XmlPath,

                }).ToList()
            };

            return map;
        }

        // PUT: api/Machine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachine(int id, MachineDTO machine)
        {
            if (id != machine.Id)
            {
                return BadRequest();
            }

            _context.Entry(machine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!MachineExists(id))
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

        // POST: api/Machine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Machine>> PostMachine(MachineDTO machineDTO)
        {

            if (machineDTO == null)
            {
                return 
            }

            Machine machine = new Machine();

            machine.MachineJobs = (ICollection<MachineJob>)machineDTO.MachineJobs;
            machine.Type = machineDTO.Type;
            machine.YearBuilt = machineDTO.YearBuilt;


            _context.Machines.Add(machine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMachine", new { id = machine.Id }, machine);
        }

        // DELETE: api/Machine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var machine = await _context.Machines.FindAsync(id);
            if (machine == null)
            {
                return NotFound();
            }

            _context.Machines.Remove(machine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MachineExists(int id)
        {
            return _context.Machines.Any(e => e.Id == id);
        }
    }
}
