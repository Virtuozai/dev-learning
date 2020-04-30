using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dev_learning.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dev_learning.Controllers
{
    [Route("api/[controller]")]
    class TeamsController : Controller
    {
        private readonly TeamContext _context;

        public TeamsController(TeamContext context)
        {
            _context = context;
        }

        // GET: api/Team
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }

        // Post api/Teams
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam([FromBody]Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = team.Id }, team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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
        public async Task<ActionResult<Team>> DeleteComment(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return team;       }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(c => c.Id == id);
        }
    }
}
