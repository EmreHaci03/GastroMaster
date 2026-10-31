using FluentValidation;
using GastroMaster.DtoLayer.DTOS.SocialMediaDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroMaster.BusinessLayer.FluentValidation.SocailMediaValidator
{
    public class CreateSocialMediaValidator:AbstractValidator<CreateSocialMediaDto>
    {
        public CreateSocialMediaValidator()
        {
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
