using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataPollingApi.Models;

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
        public ActionResult<List<MachineDTO>> GetMachines()
        {
            var machines = _context.Machines
            .Select(m => new MachineDTO
            {
                Id = m.Id,
                Type = m.Type,
                YearBuilt = m.YearBuilt
            })
            .ToList();
            return machines;
        }

        // GET: api/Machine/5
        [HttpGet("{id}")]
        public  ActionResult<MachineDTO> GetMachine(int id)
        {
            var machine =  _context.Machines.Find(id);

            if (machine == null)
            {
                return NotFound();
            }

            var map = new MachineDTO
            {
                Id = machine.Id,
                Type = machine.Type,
                YearBuilt = machine.YearBuilt
            };

            return map;
        }

        // PUT: api/Machine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMachine(int id, Machine machine)
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
        public async Task<ActionResult<Machine>> PostMachine(Machine machine)
        {
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
