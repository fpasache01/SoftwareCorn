using Microsoft.AspNetCore.Mvc;
using Context.Models;
using Services;

namespace ApiCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _salesService;

        public SalesController(ISaleService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            try
            {
                var sales = await _salesService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            try
            {
                var sale = await _salesService.GetSaleByIdAsync(id);

                if (sale == null)
                {
                    return NotFound();
                }

                return Ok(sale);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Throttle(1, 60)]//requirement!
        public async Task<ActionResult<Sale>> CreateSale([FromBody] Sale sale)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdSale = await _salesService.CreateSaleAsync(sale);
                return CreatedAtAction(nameof(GetSale), new { id = createdSale.SaleId }, createdSale);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    
    }
}