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
    public class TodoController : ControllerBase
    {
        private readonly ProjektManagerContext _context;
        public TodoController()
        {
            _context =  new();
        }
        [HttpGet]
        public IActionResult Get()
        {
            var todos = _context.Todos.ToList();

            if (todos == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(todos);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = _context.Todos.Where(t => t.TodoId == id).ToList();

            if (todo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(todo);
            }
        }

        [HttpPost]
        public IActionResult Post([Bind("TodoId,Name,IsComplete")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todo);
                _context.SaveChanges();
                return Ok("Sucess");
            }
            return NotFound();
        }

        [HttpPut]
        public IActionResult Put(int id, [Bind("TodoId,Name,IsComplete")] Todo todo)
        {
            if (id != todo.TodoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todo);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.TodoId))
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
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
            }
            
            _context.SaveChanges();
            return Ok();
        }
        private bool TodoExists(int id)
        {
          return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}
