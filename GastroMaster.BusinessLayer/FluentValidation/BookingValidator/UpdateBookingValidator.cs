using FluentValidation;
using GastroMaster.DtoLayer.DTOS.BookingDtos;

namespace GastroMaster.BusinessLayer.FluentValidation.BookingValidator
{
    public class UpdateBookingValidator:AbstractValidator<UpdateBookingDto>
    {
        public UpdateBookingValidator()
        {
            RuleFor(x => x.BookingId)
                .GreaterThan(0).WithMessage("Güncellenecek kayıt bulunamadı");

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Ad soyad boş geçilemez")
               .MinimumLength(3).WithMessage("Ad soyad en az 3 karakter olmalıdır")
               .MaximumLength(50).WithMessage("Ad soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon boş geçilemez")
                .Matches(@"^0?5\d{2}\s?\d{3}\s?\d{2}\s?\d{2}$").WithMessage("Geçerli bir telefon numarası giriniz (05xx xxx xx xx)");

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("E-posta boş geçilemez")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.PersonCount)
                .GreaterThan(0).WithMessage("Lütfen kişi sayısı seçiniz")
                .LessThanOrEqualTo(20).WithMessage("En fazla 20 kişilik rezervasyon yapılabilir");

            RuleFor(x => x.Date)
                .Must(date => date > DateTime.Now).WithMessage("Geçmiş bir tarihe rezervasyon yapılamaz")
                .Must(date => date < DateTime.Now.AddMonths(3)).WithMessage("En fazla 3 ay sonrasına rezervasyon yapılabilir");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Özel istek en fazla 250 karakter olabilir");
        }
    }
}
