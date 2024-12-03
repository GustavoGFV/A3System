using A3System.Dbo.Dto.User;
using A3System.Interface;
using A3System.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService) => _usuarioService = usuarioService;

        /// <summary>
        /// Get Usuario por ID, vai ser utilizada pelo proprio sistema para consultar o usuario atual.
        /// </summary>
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

        /// <summary>
        /// Get Usuarios é uma camada da API aonde apenas os Admin e Devs do sistema poderiam acessar 
        /// pois seria uma informação inalcansavel pelo usuario padrão
        /// </summary>
        [Authorize(Roles = "Admin, Dev")]
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
        /// <summary>
        /// Atualização das informações gerais de usuario, seria utilizado para atualizar o UserName ou algo assim em um sistema comum
        /// </summary>
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

        /// <summary>
        /// Atualização da senha do usuario com as inforamções na descrição do Swagger do que deve ser obrigatoriamente preenchido
        /// </summary>
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
