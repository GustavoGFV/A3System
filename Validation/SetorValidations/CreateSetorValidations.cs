using A3System.Dbo.Dto.Setor;
using A3System.Resources;
using A3System.Utils;
using FluentValidation;

namespace A3System.Validation.UserValidations
{
    public class CreateSetorValidations : AbstractValidator<CreateSetorDto>
    {
        public CreateSetorValidations()
        {
            RuleFor(x => x).NotNull().NotEmpty().WithMessage(ErrorTranslation.FormNotFilled).WithErrorCode(ErrorCodes.SetorObjectErrorCode);           
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(ErrorTranslation.SetorNameNull).WithErrorCode(ErrorCodes.SetorNameErrorCode);
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage(ErrorTranslation.SetorNameNull).WithErrorCode(ErrorCodes.SetorNameErrorCode);
        }
    }
}