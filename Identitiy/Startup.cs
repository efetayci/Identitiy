using Identitiy.Context;
using Identitiy.CustomValidator;
using Identitiy.MyContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identitiy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<KursContext>();
            services.AddIdentity<AppUser, AppRole>(
                opt=> { //parola k�s�tlamalar�
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);//locklama s�resi
                opt.Lockout.MaxFailedAccessAttempts = 3;//ka� defa yanl�� girince blocklayim.
                    }
                ).AddPasswordValidator<CustomPasswordValidator>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<KursContext>();
            
            //Yukar�daki error describer bizim ekledi�imiz kontrolleri i�ermesi i�in

            ///<summary>
            ///cookie ayarlar�
            /// </summary>
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");// yetkim olmayan bir yere gitmek istedi�im zaman y�nlendirme.
                opt.Cookie.HttpOnly = true; // scriptte cookie verisine ula�amaz
                opt.Cookie.Name = "KursCookie"; //Cookienin ismi
                opt.Cookie.SameSite = SameSiteMode.Strict; //Cookie sub domainler bile er�emez, lux yaparsam herkes eri�ir.
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;//http ve https ile �al���r
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);//Cookie �mr�
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
