using Identitiy.MyContext;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy.Context
{
    public class KursContext : IdentityDbContext<AppUser,AppRole,int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-LOIEK0I ;database=IdentityLearning ;integrated security=true;");
            base.OnConfiguring(optionsBuilder);
        }

        
    }
}
