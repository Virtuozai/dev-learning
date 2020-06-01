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

        // GET: api/UserSubjects/5
        [HttpGet("{id}")]
        public async Task<UserSubject> GetUserSubjectsById(int id)
        {
            var userSubject = await _context.UserSubjects.FindAsync(id);
            return userSubject;
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
            var days = userSubject.EndDateTime.Day - userSubject.StartDateTime.Day + 1;
            var month = userSubject.StartDateTime.Month;
            var user = _context.Users.Find(userSubject.UserId);
            var userSubjectsForMonth = _context.UserSubjects.Where(u => u.UserId == user.Id)
                                                            .Where(u => u.StartDateTime.Month == month)
                                                            .ToList();
                                                            
            for (int i = 0; i < userSubjectsForMonth.Count; i++) {
                days += (userSubjectsForMonth[i].EndDateTime.Day - userSubjectsForMonth[i].StartDateTime.Day + 1);
            }

            if (days > 5)
            {
                Response.StatusCode = 400;
                return Content("Not enough learning days left for this month");
            }
            if(userSubject.StartDateTime > userSubject.EndDateTime)
            {
                Response.StatusCode = 400;
                return Content("Incorrect start and end datetimes");
            }
            var userSubjects = _context.UserSubjects.Where(u => u.UserId == userSubject.UserId)
                                                    .Where(s => s.SubjectId == userSubject.SubjectId)
                                                    .Where(s => s.StartDateTime.Date == userSubject.StartDateTime.Date)
                                                    .ToList();
            if (userSubjects.Count > 0)
            {
                Response.StatusCode = 400;
                return Content("This user subject for this date already exists");
            }
            user.LearningDaysLeft -= days;

            _context.Users.Update(user);
            _context.UserSubjects.Add(userSubject);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/UserSubject/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserSubject(int id, UserSubject userSubject)
        {
            if (id != userSubject.Id)
            {
                return BadRequest();
            }

            List<UserSubject> userSubjects = new List<UserSubject>();

            if (userSubject.IsLearned == true)
            {
                userSubjects = _context.UserSubjects.Where(u => u.UserId == userSubject.UserId)
                                                        .Where(s => s.SubjectId == userSubject.SubjectId).ToList();
                for (int i = 0; i < userSubjects.Count; i++) userSubjects[i].IsLearned = true;
            }
            else userSubjects.Add(userSubject);

            for(int i = 0; i < userSubjects.Count; i++)
            {
                _context.Entry(userSubjects[i]).State = EntityState.Modified;
            }

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
