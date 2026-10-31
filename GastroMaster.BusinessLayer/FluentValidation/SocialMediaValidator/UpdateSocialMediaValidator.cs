using FluentValidation;
using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;

namespace GastroMaster.BusinessLayer.FluentValidation.SocialMediaValidator
{
    public class UpdateSocialMediaValidator:AbstractValidator<UpdateSocialMediaDto>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(x => x.SocialMediaId)
                .GreaterThan(0).WithMessage("Lütfen Geçerli Bir Sosyal Medya Alanı Giriniz");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık boş geçilemez")
                .MaximumLength(50).WithMessage("Başlık en fazla 50 karakter olabilir");

            RuleFor(x => x.Icon)
                .NotEmpty().WithMessage("İkon boş geçilemez")
                .MaximumLength(50).WithMessage("İkon en fazla 50 karakter olabilir");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("Bağlantı adresi boş geçilemez")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) && (uri.Scheme == "http" || uri.Scheme == "https"))
                .WithMessage("Geçerli bir bağlantı adresi giriniz (https:// ile başlamalı)");

            
        }
    }
}
