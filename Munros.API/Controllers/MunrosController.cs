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
                if (!queryParameters.MinMaxHeightValid())
                {
                    return BadRequest("Max height must be greater than min height");
                }

                var results = await _repository.GetMunrosAsync(queryParameters);

                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Munro>> GetMunroAsync(int id)
        {
            try
            {
                var munro = await _repository.GetMunroAsync(id);

                if (munro == null) return NotFound();

                return Ok(munro);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Munro>> PostMunro([FromBody] Munro munro)
        {
            try
            {
                var existing =  await _repository.GetMunroAsync(munro.Id);

                if (existing != null)
                {
                    return BadRequest("Id already in use");
                }

                await _repository.AddMunroAsync(munro);

                return CreatedAtAction(
                    "GetMunro",
                    new { id = munro.Id },
                    munro
                );

                //return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMunro(int id, [FromBody] Munro munro)
        {
            try
            {
                var existingMunro = await _repository.GetMunroAsync(id);

                if (existingMunro == null) return NotFound($"Could not find munro with id of {id}");

                if (await _repository.UpdateMunroAsync(existingMunro, munro) > 0)
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }

            return BadRequest("Failed to update the munro");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMunroAsync(int id)
        {
            try
            {
                var existingMunro = await _repository.GetMunroAsync(id);

                if (existingMunro == null) return NotFound($"Could not find munro with id of {id}");

                if (await _repository.DeleteMunroAsync(existingMunro) > 0)
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "API exception");
            }

            return BadRequest("Failed to delete the munro");
        }
    }
}
