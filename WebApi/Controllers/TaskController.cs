using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFC6_1;
using Task = EFC6_1.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ProjektManagerContext _context;
        public TaskController()
        {
            _context = new();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var tasks = _context.Tasks.ToList();

            if (tasks == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(tasks);
            }
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) {

            var task = _context.Tasks.Where(t => t.TaskId == id).ToList();

            if (task == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(task);
            }
        }

        [HttpPost]
        public IActionResult Post(Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                _context.SaveChanges();
                return Ok("Sucess");
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult Put(int id, Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            _context.SaveChanges();
            return Ok();
        }
        private bool TaskExists(int id)
        {
          return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}
