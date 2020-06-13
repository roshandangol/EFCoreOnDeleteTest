using EFCoreOnDeleteTest.Data;
using EFCoreOnDeleteTest.MOdel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreOnDeleteTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPoliciesController : ControllerBase
    {
        private readonly EFCoreOnDeleteTestContext _context;

        public UserPoliciesController(EFCoreOnDeleteTestContext context)
        {
            _context = context;
        }

        // GET: api/UserPolicies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserPolicy>>> GetUserPolicies()
        {
            return await _context.Userpolicy.ToListAsync();
        }

        // GET: api/UserPolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserPolicy>> GetUserPolicy(int id)
        {
            var userPolicy = await _context.Userpolicy.FindAsync(id);

            if (userPolicy == null)
            {
                return NotFound();
            }

            return userPolicy;
        }

        // PUT: api/UserPolicies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserPolicy(int id, UserPolicy userPolicy)
        {
            if (id != userPolicy.Id)
            {
                return BadRequest();
            }

            _context.Entry(userPolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserPolicyExists(id))
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

        // POST: api/UserPolicies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserPolicy>> PostUserPolicy(UserPolicy userPolicy)
        {
            _context.Userpolicy.Add(userPolicy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserPolicy", new { id = userPolicy.Id }, userPolicy);
        }

        // DELETE: api/UserPolicies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserPolicy>> DeleteUserPolicy(int id)
        {
            var userPolicy = await _context.Userpolicy.FindAsync(id);
            if (userPolicy == null)
            {
                return NotFound();
            }

            _context.Userpolicy.Remove(userPolicy);
            await _context.SaveChangesAsync();

            return userPolicy;
        }

        private bool UserPolicyExists(int id)
        {
            return _context.Userpolicy.Any(e => e.Id == id);
        }
    }
}
