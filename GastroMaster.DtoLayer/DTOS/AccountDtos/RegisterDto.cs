
using System.ComponentModel.DataAnnotations;

namespace GastroMaster.DtoLayer.DTOS.AccountDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Ad boş bırakılamaz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad boş bırakılamaz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-posta boş bırakılamaz")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre tekrarı boş bırakılamaz")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }
}
