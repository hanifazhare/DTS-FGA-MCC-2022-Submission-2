using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventoryManagementWebAPI.ViewModels;
using InventoryManagementWebAPI.Repositories.Datas;

namespace InventoryManagementWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ItemRepository _itemRepository;

        public ItemController(ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllItemService()
        {
            var item = await _itemRepository.GetItem();
            if (item.Count == 0)
                return Ok(new { statusCode = 200, data = "null" });

            return Ok(new { statusCode = 200, data = item });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItemByIdService(int id)
        {
            var item = await _itemRepository.GetItem(id);
            if (item == null)
                return Ok(new { statusCode = 200, data = "null" });

            return Ok(new { statusCode = 200, data = item });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("edit")]
        public async Task<ActionResult> EditItemService(ItemViewModel item)
        {
            var result = await _itemRepository.PutItem(item);

            if (result == 3)
                return Ok(new { statusCode = 200, message = "Failed update data, item id not found" });
            else if (result == 2)
                return Ok(new { statusCode = 200, message = "Failed update data, item code already exist" });
            else if (result == 1)
                return Ok(new { statusCode = 200, message = "Success update data" });

            return BadRequest(new { statusCode = 400, message = "Failed update data" });
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("create")]
        public async Task<ActionResult> CreateItemService(ItemViewModel item)
        {
            var result = await _itemRepository.PostItem(item);

            if (result == 2)
                return Ok(new { statusCode = 200, message = "Failed create data, item code already exist" });
            else if (result == 1)
                return Ok(new { statusCode = 200, message = "Success create data" });

            return BadRequest(new { statusCode = 400, message = "Failed create data" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteItemService(int id)
        {
            var result = await _itemRepository.DeleteItem(id);

            if (result == 2)
                return Ok(new { statusCode = 200, message = "Failed delete data, item id not found" });
            else if (result == 1)
                return Ok(new { statusCode = 200, message = "Success delete data" });

            return BadRequest(new { statusCode = 400, message = "Failed delete data" });
        }
    }
}
