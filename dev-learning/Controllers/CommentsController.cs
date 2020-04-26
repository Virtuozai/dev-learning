using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dev_learning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dev_learning.Controllers
{
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {
        private readonly MyDbContext _context;

        public CommentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // GET: api/Comments/User/5
        [HttpGet("User/{userId}")]
        public async Task<List<Comment>> GetUserComments(int userId)
        {
            return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        }

        // GET: api/Comments/Subject/5
        [HttpGet("Subject/{subjectId}")]
        public async Task<List<Comment>> GetSubjectComments(int subjectId)
        {
            return await _context.Comments.Where(c => c.SubjectId == subjectId).ToListAsync();
        }

        // POST api/Comments
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment([FromBody]Comment comment)
        {
            comment.User = _context.Users.FirstOrDefault(x => x.Id == comment.UserId);

            comment.User.Comments.Add(comment);
            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // DELETE api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }
    }
}
