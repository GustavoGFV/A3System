using A3System.Dbo.Dto.User;
using A3System.Resources;
using A3System.Utils;
using FluentValidation;

namespace UsuariosAPI.Validation.UserValidations
{
    public class UpdateUserValidations : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidations()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Surname).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.UserObjectErrorCode);
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage(ErrorTranslation.UserEmailNotFilled).WithErrorCode(ErrorCodes.UserEmailErrorCode);

            RuleSet("Senha", () =>
            {
                RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage(ErrorTranslation.PasswordNotFilled).WithErrorCode(ErrorCodes.UserSenhaErrorCode);
                RuleFor(x => x.RePassword).NotNull().NotEmpty().Matches(x => x.Password).WithMessage(ErrorTranslation.PasswordDoesntMatch).WithErrorCode(ErrorCodes.UserSenhaErrorCode);
            });
        }
    }
}
