using System.ComponentModel.DataAnnotations;

namespace WebSitesi.Models
{
    public class ContactMessage
    {
        [Required(ErrorMessage = "Ad soyad gereklidir.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mesaj gereklidir.")]
        [StringLength(500, ErrorMessage = "Mesaj 500 karakteri geçemez.")]
        public string Message { get; set; }
    }
}
