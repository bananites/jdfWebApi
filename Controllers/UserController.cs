using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataPollingApi.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

// TODO Maybe I want to have different routes for User and User with Jobs
namespace DataPollingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly S31JdfMachineHandlerContext _context;

        public UserController(S31JdfMachineHandlerContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var users = _context.Users
            .Include(u => u.UserJobs)
            .ThenInclude(uj => uj.Job)
            .Select(u => new UserDTO
            {
                Id = u.Id,
                Username = u.Username,
                UserJobs = u.UserJobs
                .Where(uj => uj.Job != null)
                .Select(uj => new JobDTO
                {
                    Id = uj.Job.Id,
                    Name = uj.Job.Name,
                    Status = uj.Job.Status,
                    XmlPath = uj.Job.XmlPath,

                }).ToList()
            })
            .ToList();
            return users;
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var user = _context.Users
            .Include(u => u.UserJobs)
            .ThenInclude(uj => uj.Job)
            .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var map = new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                UserJobs = user.UserJobs
                .Where(uj => uj.Job != null)
                .Select(uj => new JobDTO
                {
                    Id = uj.Job.Id,
                    Name = uj.Job.Name,
                    Status = uj.Job.Status,
                    XmlPath = uj.Job.XmlPath,

                }).ToList()


            };

            return map;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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


        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            if (user == null || user.Username == null || user.Password == null)
            {
                return BadRequest("User was not created");
            }
            user.Password = HashPasswort(user.Username, user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private string HashPasswort(string salt, string pwd)
        {

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pwd,
                salt: Encoding.UTF8.GetBytes(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 /8
            ));

            return hashed;

        }
    }
}
