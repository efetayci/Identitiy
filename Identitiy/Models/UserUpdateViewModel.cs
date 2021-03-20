using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Models
{
    public class UserUpdateViewModel
    {
        [Required(ErrorMessage ="Email alanı gereklidir.")]
        [EmailAddress(ErrorMessage ="Lütfen geçerli bir email adress giriniz.")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = "Name alanı gereklidir.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname alanı gereklidir.")]
        public string SurName { get; set; }
        public string PictureUrl { get; set; }
        
    }
}
