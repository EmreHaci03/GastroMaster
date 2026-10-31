using FluentValidation;
using GastroMaster.DtoLayer.DTOS.CategoryDtos;

namespace GastroMaster.BusinessLayer.FluentValidation.CategoryValidator
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {

            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori Adı boş geçilemez")
                .MinimumLength(5).WithMessage("Kategori Adı en az 5 karakter olmalıdır")
                .MaximumLength(50).WithMessage("Kategori Adı en fazla 50 karakter olabilir");
        }
    }
}
