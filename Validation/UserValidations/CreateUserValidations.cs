using A3System.Dbo.Dto.User;
using A3System.Resources;
using A3System.Utils;
using FluentValidation;

namespace UsuariosAPI.Validation.UserValidations
{
    public class CreateUserValidations : AbstractValidator<CreateUserDto>
    {
        /// <summary>
        /// FluentValidation prove diversos tipos de validação ja pre-feitas junto de configurações feitas pelo
        /// proprio usuario
        /// Nesta Validação ele olha se todos os campos estão ou não vazios e em caso de erro ele tem uma mensagem diferenciada feita pelo dev
        /// O RuleSet separa uma validação das outras, para que ela seja chamada diretamente sem ser chamada pelas outras, sendo assim
        /// um processo modular de validação, todas podem ser executadas juntas, mas o ruleset pode ser utilizado separamente 
        /// </summary>
        public CreateUserValidations()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(ErrorTranslation.UserEmailNotFilled).WithErrorCode(ErrorCodes.UserEmailErrorCode);

            RuleSet("Senha", () =>
            {
                RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(ErrorTranslation.PasswordNotFilled).WithErrorCode(ErrorCodes.UserSenhaErrorCode);
                RuleFor(x => x.RePassword).NotNull().NotEmpty().Matches(x => x.Password).WithMessage(ErrorTranslation.PasswordDoesntMatch).WithErrorCode(ErrorCodes.UserSenhaErrorCode);
            });
        }
    }
}