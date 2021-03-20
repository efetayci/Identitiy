using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Models
{
    public class UserSignUpViewModel
    {
        
        [Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parola  Boş Geçilemez")]
        public string Password { get; set; }
        
        [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ad Boş Geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad Boş Geçilemez")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Mail Boş Geçilemez")]
        public string Email { get; set; }
    }
}
