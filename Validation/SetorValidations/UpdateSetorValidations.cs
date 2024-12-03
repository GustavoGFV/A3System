using A3System.Dbo.Dto.Setor;
using A3System.Resources;
using A3System.Utils;
using FluentValidation;

namespace A3System.Validation.UserValidations
{
    public class UpdateSetorValidations : AbstractValidator<UpdateSetorDto>
    {
        /// <summary>
        /// FluentValidation prove diversos tipos de validação ja pre-feitas junto de configurações feitas pelo
        /// proprio usuario
        /// Nesta Validação ele olha se todos os campos estão ou não vazios e em caso de erro ele tem uma mensagem diferenciada feita pelo dev 
        /// </summary>
        public UpdateSetorValidations()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.SetorObjectErrorCode);
            RuleFor(x => x.Id).NotNull().GreaterThan(0).WithMessage(ErrorTranslation.SetorNotFilled).WithErrorCode(ErrorCodes.SetorIdErrorCode);
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ErrorTranslation.SetorNameNull).WithErrorCode(ErrorCodes.SetorNameErrorCode);
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage(ErrorTranslation.SetorNameNull).WithErrorCode(ErrorCodes.SetorNameErrorCode);
        }
    }
}
