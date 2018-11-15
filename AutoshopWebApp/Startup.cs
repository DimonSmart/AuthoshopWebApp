using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using AutoshopWebApp.Authorization.Handlers;

namespace AutoshopWebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                //.AddDefaultUI()
                .AddDefaultTokenProviders()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSingleton<IAuthorizationHandler, WorkerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, TransactionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, PositionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, CarAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, BuyerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SellerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StateAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, ExpertiseAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, SparePartAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //Локализационный костыль

            app.UseRequestLocalization("ru-RU");

            //Конец костыля

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
