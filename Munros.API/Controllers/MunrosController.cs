using Microsoft.AspNetCore.Mvc;

namespace Munros.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MunrosController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMunros()
        {
            return Ok("OK.");
        }
    }
}
