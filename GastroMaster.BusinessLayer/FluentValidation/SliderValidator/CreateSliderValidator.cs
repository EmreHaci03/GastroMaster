using FluentValidation;
using GastroMaster.DtoLayer.DTOS.SliderDtos;


namespace GastroMaster.BusinessLayer.FluentValidation.SliderValidator
{
    public class CreateSliderValidator:AbstractValidator<CreateSliderDto>
    {
        public CreateSliderValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
                .MaximumLength(300).WithMessage("Açıklama en fazla 300 karakter olabilir");
        }
    }
}
