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
    public class LinkController : ControllerBase
    {
        private readonly ProjektManagerContext _context;
        public LinkController()
        {
            _context =  new();
        }

        [HttpPut("tasktoteam")]
        public IActionResult Put(int taskId, int teamId)
        {
            if(TeamExists(teamId) && TaskExists(taskId))
            {
                var task = _context.Tasks.Where(t => t.TaskId == taskId).First();
                var team = _context.Teams.Where(t => t.TeamId == teamId).First();
                team.CurrentTask = task;
                _context.SaveChanges();

                return Ok();
            }
            return NotFound();
        }

        private bool TaskExists(int id)
        {
          return _context.Tasks.Any(e => e.TaskId == id);
        }
        private bool TeamExists(int id)
        {
          return _context.Teams.Any(e => e.TeamId == id);
        }
    }
}
