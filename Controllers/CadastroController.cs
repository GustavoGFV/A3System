using A3System.Dbo.Dto.User;
using A3System.Interface;
using A3System.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace A3System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CadastroController : ControllerBase
    {
        private IRegisterService _cadastroServices;
        private readonly IValidator<CreateUserDto> _validator;

        public CadastroController(IRegisterService cadastroServices, IValidator<CreateUserDto> validator)
        {
            _cadastroServices = cadastroServices;
            _validator = validator;
        }

        /// <summary>
        /// Cadastrar Usuario
        /// Ele valida todos os campos no inicio para evitar o processamento com campos falhos
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CadastraUsuario([FromBody] CreateUserDto usuarioDto)
        {
            try
            {
                await _validator.ValidateAsync(usuarioDto, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeAllRuleSets().IncludeRulesNotInRuleSet();
                });

                await _cadastroServices.CadastrarUsuario(usuarioDto);

                return Ok(SucessTranslation.UserRegistered);
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
