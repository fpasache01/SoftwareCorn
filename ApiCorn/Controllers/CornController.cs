using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCorn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CornController : ControllerBase
    {
        [HttpPost]
        [Throttle(1, 60)] 
        public async Task<IActionResult> DummyFunction([FromBody] string dummyInput)
        {     
            await Task.Delay(100); 

            var dummyResult = new
            {
                Message = "This is a dummy response",
                ReceivedInput = dummyInput, 
                Timestamp = DateTime.UtcNow
            };

            return Ok(dummyResult);
        }

    }
}
