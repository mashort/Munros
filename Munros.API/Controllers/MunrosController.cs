using Microsoft.AspNetCore.Mvc;
using Munros.Core.Interfaces;
using System.Threading.Tasks;

namespace Munros.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MunrosController : ControllerBase
    {
        private readonly IMunroRepository _repository;

        public MunrosController(IMunroRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMunros()
        {
            return Ok("OK.");
        }
    }
}
