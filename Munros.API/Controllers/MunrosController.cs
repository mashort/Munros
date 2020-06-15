using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Munros.Core.Entities;
using Munros.Core.Interfaces;
using System;
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
        public async Task<IActionResult> GetMunros([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                var results = await _repository.GetMunrosAsync(queryParameters);

                return Ok(results);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }
        }
    }
}
