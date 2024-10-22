using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI1.Model;

namespace WebAPI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // Create => POST
        // Read => GET
        // Update => PUT
        // Delete => DELETE

        // In memory storafe for simplicity
        private static readonly List<Item> _items = [];

        // GET api/tasks
        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get()
        {
            return Ok(_items);
        }

        // GET api/tasks/1
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // POST api/tasks
        [HttpPost]
        public ActionResult Post([FromBody] Item item)
        {
            _items.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT api/tasks/1
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var itemToUpdate = _items.FirstOrDefault(x => x.Id == id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Title = item.Title;
            itemToUpdate.Description = item.Description;
            itemToUpdate.IsCompleted = item.IsCompleted;

            return NoContent();
        }

        // DELETE api/tasks/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var itemToDelete = _items.FirstOrDefault(x =>x.Id == id);
            if(itemToDelete == null)
            {
                return NotFound();
            }
            _items.Remove(itemToDelete);
            return NoContent();
        }
    }
}
