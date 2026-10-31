using FluentValidation;
using GastroMaster.DtoLayer.DTOS.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GastroMaster.BusinessLayer.FluentValidation.ProductValidator
{
    public class CreateProductValidator:AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş geçilemez")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır")
                .MaximumLength(100).WithMessage("Ürün adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş geçilemez")
                .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalıdır")
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır")
                .LessThanOrEqualTo(100000).WithMessage("Fiyat en fazla 100.000 olabilir");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Görsel adresi boş geçilemez");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Lütfen bir kategori seçiniz");
        }
    }
}
