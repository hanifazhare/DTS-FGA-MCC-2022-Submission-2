using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryManagementWebAPI.Contexts;
using InventoryManagementWebAPI.Models;
using InventoryManagementWebAPI.ViewModels;

namespace InventoryManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly DBContext _context;

        public ItemController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            var item = await _context.Items.ToListAsync();
            if (item.Count == 0)
                return Ok(new { statusCode = 200, data = "null" });

            return Ok(new { statusCode = 200, data = item });
        }

        // GET: api/Item/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
                return Ok(new { statusCode = 200, data = "null" });

            return Ok(new { statusCode = 200, data = item });
        }

        // PUT: api/Item/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<IActionResult> PutItem(ItemViewModel item)
        {
            var itemData = await _context.Items.FindAsync(item.id);

            if (itemData == null)
                return NotFound(new { statusCode = 404, message = "Data not found" });

            itemData.code = item.code;
            itemData.name = item.name;
            itemData.available_quantity = item.available_quantity;
            itemData.notes = item.notes;

            _context.Items.Update(itemData);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return Ok(new { statusCode = 200, message = "Success update data" });

            return BadRequest(new { statusCode = 400, message = "Failed update data" });
        }

        // POST: api/Item
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(ItemViewModel item)
        {
            var itemCode = await _context.Items.FindAsync(item.code);

            if (itemCode == null)
                return BadRequest(new { statusCode = 400, message = "Failed create data" });

            _context.Items.Add(new Item
            {
                code = item.code,
                name = item.name,
                available_quantity = item.available_quantity,
                notes = item.notes
            });

            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return Ok(new { statusCode = 200, message = "Success create data" });

            return BadRequest(new { statusCode = 400, message = "Failed create data" });
        }

        // DELETE: api/Item/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
                return NotFound(new { statusCode = 404, message = "Data not found" });

            _context.Items.Remove(item);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return Ok(new { statusCode = 200, message = "Success delete data" });

            return BadRequest(new { statusCode = 400, message = "Failed delete data" });
        }
    }
}
