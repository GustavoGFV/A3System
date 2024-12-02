using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using A3System.Interface;
using A3System.Resources;
using A3System.Dbo.Dto.Setor;
using Microsoft.AspNetCore.Authorization;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SetorController : ControllerBase
    {
        private ISetorService _setorServices;

        public SetorController(ISetorService setorServices)
        {
            _setorServices = setorServices;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CadastraSetor([FromBody] CreateSetorDto setor)
        {
            try
            {
                await _setorServices.CreateSetor(setor);

                return Ok(SucessTranslation.SetorRegistered);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Errors.First().ErrorMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSetorById(int id)
        {
            try
            {
                var result = await _setorServices.GetSetorById(id);
                if (result == null) return BadRequest();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }
        [Authorize]
        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _setorServices.GetAll();
                if (!result.Any()) return BadRequest();
                return Ok(result);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        [Authorize]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateSetor([FromBody] UpdateSetorDto setor)
        {
            try
            {
                await _setorServices.UpdateSetor(setor);

                return Ok(SucessTranslation.SetorUpdated);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Errors.First().ErrorMessage);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }
    }
}