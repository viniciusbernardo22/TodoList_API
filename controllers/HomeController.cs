using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public IActionResult Get([FromServices] AppDbContext context) => Ok(context.Todos.ToList());


        [HttpGet("/{id:int}")]
        public IActionResult GetById(int id, [FromServices] AppDbContext context)
        {
            TodoModel todo = context.Todos.FirstOrDefault(todo => todo.Id == id);

            if(todo == null) return NotFound();
            
            return Ok(todo);
            
        }


        [HttpPost("/")]
        public IActionResult Post([FromBody] TodoModel todo, [FromServices] AppDbContext context)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
            return Created($"/{todo.Id}", todo);
        }

        [HttpPut("/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel todo,
            [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(todo => todo.Id == id);

            if (model == null) return NotFound();

            model.Title = todo.Title;
            model.Done = todo.Done;

            context.Todos.Update(model);
            context.SaveChanges();

            return Ok(model);
        }

        [HttpDelete("/{id:int}")]
        public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
        {
            var model = context.Todos.FirstOrDefault(todo => todo.Id == id);
            if(model == null) return NotFound();
            context.Todos.Remove(model);
            context.SaveChanges();
            return Ok(model);
        }
    }


}