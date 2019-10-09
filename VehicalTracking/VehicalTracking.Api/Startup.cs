using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using VehicalTracking.Api.Extensions.ErrorHandling;
using VehicalTracking.Domain.ApplicationUser.Infrastructure;
using VehicalTracking.Domain.ApplicationUser.Models;
using VehicalTracking.Service;

namespace VehicalTracking.Api
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
            ConfigureJwtAuthentication(services);

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
            UserManager<AppUser> userManager,
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void AddCustomDbContext(IServiceCollection services, IConfiguration configuration)
        {
            // Application user context
            services.AddDbContext<AppUserContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("VehicalTracking.Domain.User");
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            // Vehicle context
            services.AddDbContext<VehicleContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly("VehicleTracking.Domain.Vehicle");
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            // Identity
            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<AppUserContext>()
                .AddDefaultTokenProviders();
        }

        public void ConfigureJwtAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var testing = context;
                        return Task.FromResult(0);
                    }
                };
            });
        }

        public void RegisterAppServices(IServiceCollection services)
        {
            // Services
            services.RegisterServices();
        }
    }
}
