using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AspNetCoreCursovaya.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreCursovaya.helpingClasses;

namespace AspNetCoreCursovaya
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
            services.AddControllersWithViews(); // Добавление сервиса для обработки представлений

            services.AddAuthorization(); // Добавление сервиса для внедрения авторизации в конвейер обработки запросов

            services.AddHttpContextAccessor();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Добавление сервиса для внедрения аунтефикации в конвеер обработки запросов
             .AddCookie(options =>
             {
                 options.Cookie.Name = "YourCookieName";
                 options.Cookie.HttpOnly = true;
                 options.Cookie.SameSite = SameSiteMode.Strict;
                 options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                 options.LoginPath = "/home/Index";
                 options.LogoutPath = "/home/Index";
                 options.AccessDeniedPath = "/home/Index";
                 options.ExpireTimeSpan = TimeSpan.FromDays(30);
                 
             });
            ///////////
            ///

            services.AddDistributedMemoryCache(); // Добавление сервиса для хранения информации во внетренней памяти устройства КУКИ

            // Добавление сервиса для обработки представлений
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(365);
                options.Cookie.Name = "YourSessionCookieName";
                options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.None;
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.IsEssential = true;
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(new ErrorHandlingMiddleware());
            });

            ////////////
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 21));

            services.AddDbContext<cursovayadbContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), serverVersion));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.Build();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
