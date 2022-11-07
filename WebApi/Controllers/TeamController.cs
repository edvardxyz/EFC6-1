using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFC6_1;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ProjektManagerContext _context;
        public TeamController()
        {
            _context =  new();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var teams = _context.Teams.ToList();

            if (teams == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(teams);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var team = _context.Teams.Where(t => t.TeamId == id).ToList();

            if (team == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(team);
            }
        }

        [HttpPost]
        public IActionResult Post(Team team)
        {
            if (ModelState.IsValid)
            {
                _context.Add(team);
                _context.SaveChanges();
                return Ok("Sucess");
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult Put(int id, Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(team);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok("Success");
            }
            return NotFound();
        }

        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var team = _context.Teams.Find(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }
            
            _context.SaveChanges();
            return Ok();
        }
        private bool TeamExists(int id)
        {
          return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
