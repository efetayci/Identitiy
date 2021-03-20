using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.CustomValidator
{
    public class CustomIdentityValidator :IdentityErrorDescriber
    {
        //IPasswordValidator<AppUser>
        //Iuservalidator<AppUser>
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "PasswordTooShort",
                Description = $"Parola minumum {length} karakter olmalı."
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "parola alfenumerik karakter içermelidir."
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "DuplicateUserName",
                Description = $"İlgili kullanıcı adı{userName} zaten sistemde kayıtlı."
            };
        }

    }
}
