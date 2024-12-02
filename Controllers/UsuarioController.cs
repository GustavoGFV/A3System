using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using A3System.Dbo.Dto.User;
using A3System.Interface;
using A3System.Resources;
using Microsoft.AspNetCore.Authorization;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) => _usuarioService = usuarioService;

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            try
            {
                ReadUserDto usuario = await _usuarioService.GetUsuario(id);
                if (usuario == null) return NotFound();
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<ReadUserDto> usuarios = await _usuarioService.GetUsuarios();
                if (usuarios == null) return NotFound();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return NotFound(e.InnerException != null ? e.InnerException.Message : e.Message);
            }
        }

        #region Atualizar Usuario
        [Authorize]
        [HttpPut("Atualizar")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto usuarioDto)
        {
            try
            {
                await _usuarioService.UpdateUser(usuarioDto);
                return Ok(SucessTranslation.UserUpdated);
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
        [HttpPut("Atualizar/Senha")]
        [SwaggerOperation(Description = "<strong>Obrigatório!</strong> <li> <strong> Preencher: </strong>  <ul><li>Id</li> " +
            "<li>Password</li> <li>RePassword</li></ul></li>")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdateUserDto usuarioDto)
        {
            try
            {
                await _usuarioService.UpdatePassword(usuarioDto);
                return Ok(SucessTranslation.UserPasswordUpdated);
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
        #endregion
    }
}
