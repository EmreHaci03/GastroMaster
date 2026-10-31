using FluentValidation;
using GastroMaster.DtoLayer.DTOS.AboutItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroMaster.BusinessLayer.FluentValidation.AboutItemValidator
{
    public class UpdateAboutItemValidator:AbstractValidator<UpdateAboutItemDto>
    {
        public UpdateAboutItemValidator()
        {
            RuleFor(x => x.AboutItemId)
                .GreaterThan(0).WithMessage("Güncellenecek kayıt bulunamadı");

            RuleFor(x => x.Title)
           .NotEmpty().WithMessage("Başlık boş geçilemez")
           .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır")
           .MaximumLength(500).WithMessage("Başlık en fazla 50 karakter olabilir");

            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Açıklama boş geçilemez")
               .MinimumLength(5).WithMessage("Açıklama en az 5 karakter olmalıdır")
               .MaximumLength(100).WithMessage("Açıklama en fazla 100 karakter olabilir");
        }
    }
}
