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
                opt=> { //parola kýsýtlamalarý
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);//locklama süresi
                opt.Lockout.MaxFailedAccessAttempts = 3;//kaç defa yanlýþ girince blocklayim.
                    }
                ).AddPasswordValidator<CustomPasswordValidator>().AddErrorDescriber<CustomIdentityValidator>().AddEntityFrameworkStores<KursContext>();
            
            //Yukarýdaki error describer bizim eklediðimiz kontrolleri içermesi için

            ///<summary>
            ///cookie ayarlarý
            /// </summary>
            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Home/Index");// yetkim olmayan bir yere gitmek istediðim zaman yönlendirme.
                opt.Cookie.HttpOnly = true; // scriptte cookie verisine ulaþamaz
                opt.Cookie.Name = "KursCookie"; //Cookienin ismi
                opt.Cookie.SameSite = SameSiteMode.Strict; //Cookie sub domainler bile erþemez, lux yaparsam herkes eriþir.
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;//http ve https ile çalýþýr
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);//Cookie ömrü
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
