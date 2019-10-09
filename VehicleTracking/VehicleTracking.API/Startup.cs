using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using VehicalTracking.Domain.ApplicationUser.Infrastructure;
using VehicleTracking.Api.Extensions.ErrorHandling;
using VehicleTracking.API.Extensions.Authentication;
using VehicleTracking.Common.Helpers;
using VehicleTracking.Domain.ApplicationUser.Infrastructure;
using VehicleTracking.Domain.ApplicationUser.Models;
using VehicleTracking.Service;

namespace VehicleTracking.API
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
            // Add custom db context
            AddCustomDbContext(services, Configuration);

            // Configure Jwt Authentication
            services.ConfigureJwtAuthentication(Configuration["Tokens:Issuer"],
                Configuration["Tokens:Audience"],
                Configuration["Tokens:Key"]);

            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

            // Enforce lowercase routing
            services.AddRouting(options => options.LowercaseUrls = true);

            // Register the Swagger services
            services.AddSwaggerDocument();

            // Register application services
            RegisterAppServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            // Configure handing errors globally
            app.ConfigureExceptionHandler();

            // Seed default data
            IdentityDataInitializer.SeedData(userManager, roleManager).Wait();

            // Use HTTPS Redirection middleware to redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();

            app.UseRouting();

            // Authenticate before the user accesses secure resources.
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure helpers
            ConfigureHelpers(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void AddCustomDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Application user context
            services.AddDbContext<ApplicationUserContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UserConnectionString"));
            });

            // Vehicle context
            services.AddDbContext<VehicleContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("VehicleConnectionString"));
            });

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationUserContext>()
                .AddDefaultTokenProviders();
        }

        public void RegisterAppServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Services
            services.RegisterServices();
        }

        public void ConfigureHelpers(IApplicationBuilder app)
        {
            // Inject the IHttpContextAccessor into the HttpContextHelper
            HttpContextHelper.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
        }
    }
}
