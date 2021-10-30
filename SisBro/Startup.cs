using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SisBro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath.Add("/account/facebook-login");
                    options.LoginPath.Add("/account/google-login");
                })
                .AddFacebook(options =>
                {
                    options.AppId = "6919989708026756";
                    options.AppSecret = "f2ec4158c197754290d2475fe1859e95";
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "918045892072-j3v9tj9nsp1b1h0qiukjvqvl6tepfsr4.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });

            services.AddControllersWithViews();
            services.AddRazorPages();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}