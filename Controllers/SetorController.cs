using A3System.Dbo.Dto.Setor;
using A3System.Interface;
using A3System.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Cadastro de Setor, disponivel apenas com o Bearer Token valido
        /// </summary>
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

        /// <summary>
        /// Seleção de Setor por ID do setor, disponivel apenas com o Bearer Token valido
        /// </summary>
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

        /// <summary>
        /// Seleção de todos os Setores com uma listagem, para melhor exibição, disponivel apenas com o Bearer Token valido
        /// </summary>
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

        /// <summary>
        /// Update de Setor, disponivel apenas com o Bearer Token valido
        /// </summary>
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