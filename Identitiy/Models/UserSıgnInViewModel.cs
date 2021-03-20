using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Models
{
    public class UserSıgnInViewModel
    {
        [Display(Name ="User Name")]
        [Required(ErrorMessage ="Kullanıcı Adı Boş Geçilemez")]
        public string UserName { get; set; }

        [Display(Name = "Passwords")]
        [Required(ErrorMessage = "Şifre Boş Geçilemez")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
