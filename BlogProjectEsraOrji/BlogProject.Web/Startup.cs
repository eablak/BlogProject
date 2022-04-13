using BlogProject.Infrastructure.Context;
using BlogProject.Infrastructure.Repositories.Concrete;
using BlogProject.Infrastructure.Repositories.Interfaces.EntityTypeRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BlogProject.Web.Models.Mappers;
using BlogProject.Model.Entities.Concrete;
using BlogProject.Model.CustomValidations;

namespace BlogProject.Web
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
            services.AddControllersWithViews();

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))); // geliştiridiğimiz context nesnesi dbcontext olarak tanımlanmaktadır.

            services.AddIdentity<IdentityUser, IdentityRole>(a => { a.User.RequireUniqueEmail = true;
                                                                    a.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
            })
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>(); // ıdentity yapılanması, AppDbContext'e özel depolama

            services.ConfigureApplicationCookie(a =>
            {
                a.LoginPath = new PathString("/AppUser/login"); a.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                a.Cookie = new CookieBuilder { Name = "KullaniciCookie", SecurePolicy = CookieSecurePolicy.Always, HttpOnly = true };
            });

            services.AddAuthentication();

            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<IArticleCategoryRepository, ArticleCategoryRepository>();



            services.AddAutoMapper(typeof(Mapping));

            //// email için
            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
            //});
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();  // önce kimlik doğrulması sonra yetkilendirme
            app.UseAuthorization();  // yetkilendirme

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Member}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(name: "area", pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");


            });

           

        }
    }
}
