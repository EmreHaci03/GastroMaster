using FluentValidation;
using GastroMaster.DtoLayer.DTOS.TestimonialDtos;

namespace GastroMaster.BusinessLayer.FluentValidation.TestimonialValidator
{
    public class UpdateTestimonialValidator : AbstractValidator<UpdateTestimonialDto>
    {
        public UpdateTestimonialValidator()
        {
            RuleFor(x => x.TestimonialId)
                .GreaterThan(0).WithMessage("Güncellenecek kayıt bulunamadı");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad soyad boş geçilemez")
                .MinimumLength(3).WithMessage("Ad soyad en az 3 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Ad soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Unvan boş geçilemez")
                .MaximumLength(50).WithMessage("Unvan en fazla 50 karakter olabilir");

            RuleFor(x => x.Comment)
                .NotEmpty().WithMessage("Yorum boş geçilemez")
                .MinimumLength(20).WithMessage("Yorum en az 20 karakter olmalıdır")
                .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel adresi boş geçilemez");
        }
    }
}