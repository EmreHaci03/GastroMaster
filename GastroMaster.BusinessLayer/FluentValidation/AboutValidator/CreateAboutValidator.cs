using FluentValidation;
using GastroMaster.DtoLayer.DTOS.AboutDtos;


namespace GastroMaster.BusinessLayer.FluentValidation.AboutValidator
{
    public class CreateAboutValidator:AbstractValidator<CreateAboutDto>
    {
        public CreateAboutValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez")
                .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .MinimumLength(20).WithMessage("Açıklama en az 20 karakter olmalıdır")
                .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel adresi boş geçilemez")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("Geçerli bir görsel adresi giriniz");

        }
    }
}
