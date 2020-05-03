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
    public class UserSubjectController : ControllerBase
    {
        private readonly DevLearningContext _context;

        public UserSubjectController(DevLearningContext context)
        {
            _context = context;
        }

        // GET: api/UserSubjects/User/5
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<UserSubject>>> GetUserSubjectsByUserId(int id)
        {
            var userSubjects = await _context.UserSubjects.Where(s => s.UserId == id).ToListAsync();
            return userSubjects;
        }

        // GET: api/UserSubjects/Subject/5
        [HttpGet("Subject/{id}")]
        public async Task<ActionResult<IEnumerable<UserSubject>>> GetUserSubjectsBySubjectId(int id)
        {
            var userSubjects = await _context.UserSubjects.Where(s => s.SubjectId == id).ToListAsync();
            return userSubjects;
        }

        // POST: api/UserSubject
        [HttpPost]
        public async Task<ActionResult<UserSubject>> PostUserSubject(UserSubject userSubject)
        {
            _context.UserSubjects.Add(userSubject);
            /*userSubject.User = _context.Users.FirstOrDefault(x => x.Id == userSubject.UserId);
            userSubject.Subject = _context.Subjects.FirstOrDefault(x => x.Id == userSubject.SubjectId);
            userSubject.User.Subjects.Add()*/
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUserSubject", new { id = userSubject.Id }, userSubject);
        }


        // PUT: api/UserSubject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubject(int id, Subject userSubject)
        {
            if (id != userSubject.Id)
            {
                return BadRequest();
            }

            _context.Entry(userSubject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserSubjectExists(id))
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

        // DELETE: api/UserSubject/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserSubject>> DeleteUserSubject(int id)
        {
            var userSubject = await _context.UserSubjects.FindAsync(id);
            if (userSubject == null)
            {
                return NotFound();
            }

            _context.UserSubjects.Remove(userSubject);
            await _context.SaveChangesAsync();

            return userSubject;
        }

        private bool UserSubjectExists(int id)
        {
            return _context.UserSubjects.Any(e => e.Id == id);
        }
    }
}
