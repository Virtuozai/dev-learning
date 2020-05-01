using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dev_learning.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dev_learning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly DevLearningContext _context;

        public SubjectsController(DevLearningContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }


        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Subject>> GetSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return subject;
        }

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetSubject", new { id = subject.Id }, subject);
        }


        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.Id)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject== null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        //GET: api/Subjects/User/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetUsersSubjects(int id)
        { 
            var userSubjects = await _context.UserSubjects.Where(a => a.UserId == id).Include(i => i.Subject).Select(s=>s.Subject).ToArrayAsync();
            var subjects = userSubjects.Select(x => new
            {
                x.Id,
                x.Title,
                x.ParentId,
            });
            return Ok(subjects);

        }
        //GET: api/Subjects/Parent/5
        [HttpGet("Parent/{id}")]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects(int id)
        {
            var subjects = await _context.Subjects.Where(a => a.ParentId == id).Select(x => new { x.Id, x.Title }).ToArrayAsync();
          
            return Ok(subjects);

        }

        private bool SubjectExists(int id)
        {
            return _context.Subjects.Any(e => e.Id == id);
        }
    }
}
