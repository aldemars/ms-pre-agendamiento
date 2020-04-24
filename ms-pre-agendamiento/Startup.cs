using System;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ms_pre_agendamiento.Repository;
using ms_pre_agendamiento.Repository.Impl;
using ms_pre_agendamiento.Service;
using ms_pre_agendamiento.Service.Impl;
using Polly;

namespace ms_pre_agendamiento
{
    public class Startup
    {
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        private ILogger<Startup> _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins(
                                "http://localhost:3000",
                                "http://localhost:3000/disponibilidad",
                                "http://dev-pre-agendamiento.azurewebsites.net/")
                            .AllowAnyHeader()
                            .WithMethods("GET", "POST");
                    });
            });

            services.AddControllers(o => o.Filters.Add(new AuthorizeFilter()));

            var healthCareFacilitiesUri = Configuration.GetSection("AppSettings")["HealthCareFacilitiesUri"];
            services.AddHttpClient("HealthCareFacilitiesAPI",
                    c => c.BaseAddress = new Uri(healthCareFacilitiesUri))
                .AddTransientHttpErrorPolicy(policy =>
                    policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300)));

            services.AddTransient<IBusyCalendarTimeSlotsRepository, BusyCalendarTimeSlotsRepository>();
            services.AddTransient<IAllCalendarTimeSlotsRepository, AllCalendarTimeSlotsRepository>();
            services.AddTransient<ICalendarAvailabilityService, CalendarAvailabilityService>();
            services.AddTransient<IHealthCareFacilityService, HealthCareFacilityService>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}); });
            services.AddTransient<IRepositoryCommandExecuter, RepositoryCommandExecuter>();
            services.AddTransient<IUserRepository, UserRepository>();

            // configure jwt authentication
            string strKey = Configuration.GetSection("AppSettings")["Secret"];
            var key = Encoding.ASCII.GetBytes(strKey);
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                        //RoleClaimType = "Role"
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("isScheduler", policy => 
                    policy.RequireClaim(ClaimTypes.Role, "scheduler"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            _logger = logger;
            if (env.EnvironmentName != "Test")
            {
                CheckDatabaseMigrations();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _logger.LogInformation("In Development environment");
            }

            if (env.IsProduction() || env.IsStaging())
            {
                app.UseHttpsRedirection();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void CheckDatabaseMigrations()
        {
            try
            {
                var cnx = new SqlConnection(Configuration.GetConnectionString("database"));
                var location = Configuration.GetSection("AppSettings")["Evolve.Location"];
                var evolve = new Evolve.Evolve(cnx)
                {
                    Locations = new[] {location},
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error applying migrations :: <{ex.Message}>", ex);
                throw;
            }
        }
    }
}