using FluentValidation;
using GastroMaster.DtoLayer.DTOS.FeatureDtos;


namespace GastroMaster.BusinessLayer.FluentValidation.FeatureValidator
{
    public class CreateFeatureValidator:AbstractValidator<CreateFeatureDto>
    {
        public CreateFeatureValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş geçilemez")
            .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır")
            .MaximumLength(60).WithMessage("Başlık en fazla 60 karakter olabilir");

            RuleFor(x => x.Description)
              .NotEmpty().WithMessage("Açıklama boş geçilemez")
              .MinimumLength(3).WithMessage("Açıklama en az 3 karakter olmalıdır")
              .MaximumLength(80).WithMessage("Açıklama en fazla 80 karakter olabilir");
        }
    }
}
