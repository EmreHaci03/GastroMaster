using FluentValidation;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;


namespace GastroMaster.BusinessLayer.FluentValidation.CategoryValidator
{
    public class UpdateCategoryValidator:AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Güncellenecek kayıt bulunamadı");

            RuleFor(x => x.CategoryName)
                 .NotEmpty().WithMessage("Kategori Adı boş geçilemez")
                 .MinimumLength(5).WithMessage("Kategori Adı en az 5 karakter olmalıdır")
                 .MaximumLength(50).WithMessage("Kategori Adı en fazla 50 karakter olabilir");
        }
    }
}

