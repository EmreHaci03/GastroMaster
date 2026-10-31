using FluentValidation;
using GastroMaster.DtoLayer.DTOS.TimeLineDtos;

namespace GastroMaster.BusinessLayer.FluentValidation.TimeLineValidator
{
    public class UpdateTimeLineValidator : AbstractValidator<UpdateTimeLineDto>
    {
        public UpdateTimeLineValidator()
        {
            RuleFor(x => x.TimelineId)
                .GreaterThan(0).WithMessage("Güncellenecek kayıt bulunamadı");

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(1900).WithMessage("Yıl 1900'den küçük olamaz")
                .Must(year => year <= DateTime.Now.Year + 5).WithMessage("Yıl en fazla 5 yıl sonrası olabilir");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez")
                .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
                .MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olabilir");
        }
    }
}