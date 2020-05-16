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
    public class UserSubjectsController : ControllerBase
    {
        private readonly DevLearningContext _context;

        public UserSubjectsController(DevLearningContext context)
        {
            _context = context;
        }

        // GET: api/UserSubjects/User/5
        [HttpGet("User/{userId}")]
        public async Task<List<UserSubject>> GetUserSubjectsByUserId(int userId)
        {
            var userSubjects = await _context.UserSubjects.Include(s => s.Subject).Where(s => s.UserId == userId).ToListAsync();
            return userSubjects;
        }

        // GET: api/UserSubjects/Subject/5
        [HttpGet("Subject/{subjectId}")]
        public async Task<List<UserSubject>> GetUserSubjectsBySubjectId(int subjectId)
        {
            var userSubjects = await _context.UserSubjects.Include(u => u.User).Where(s => s.SubjectId == subjectId).ToListAsync();
            return userSubjects;
        }

        // POST: api/UserSubject
        [HttpPost]
        public async Task<IActionResult> PostUserSubject(UserSubject userSubject)
        {
            _context.UserSubjects.Add(userSubject);
            await _context.SaveChangesAsync();
            return NoContent();
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
