using FluentValidation;
using GastroMaster.DtoLayer.DTOS.AboutItemDtos;


namespace GastroMaster.BusinessLayer.FluentValidation.AboutItemValidator
{
    public class CreateAboutItemValidator:AbstractValidator<CreateAboutItemDto>
    {
        public CreateAboutItemValidator()
        {
            RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Başlık boş geçilemez")
               .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır")
               .MaximumLength(500).WithMessage("Başlık en fazla 50 karakter olabilir");

            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Açıklama boş geçilemez")
               .MinimumLength(5).WithMessage("Açıklama en az 5 karakter olmalıdır")
               .MaximumLength(70).WithMessage("Açıklama en fazla 70 karakter olabilir");

        }
    }
}
