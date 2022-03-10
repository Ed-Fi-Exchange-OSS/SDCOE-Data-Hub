using System;
using System.Collections.Generic;
using DataHub.Api.Data;
using DataHub.Api.Middleware;
using DataHub.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

namespace DataHub.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SDCOEDatahubContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("DataHub")));
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    // Middleware setup uses <authority>/.well-known/openid-configuration to
                    // validate keys, signature, issuer, etc.
                    options.Authority = Configuration["OpenId:Authority"];
                    options.Audience = Configuration["OpenId:Audience"];
                });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddScoped<IOrganizationContextProvider, OrganizationContextProvider>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IOfferingService, OfferingService>();
            services.AddScoped<ICrmContactService, CrmContactService>();
            services.AddScoped<IExtractService, ExtractService>();
            services.AddScoped<ISupportService, SupportService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEdFiRequestService, EdFiRequestService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEdFiOdsService, EdFiOdsService>();
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            var origin = configuration["Cors:AllowedOrigins"] ??
                         throw new ArgumentNullException(configuration["Cors:AllowedOrigins"],
                             "The app setting Cors:AllowedOrigins must be defined");

            var origins = origin.Split(';');

            app.UseCors(p => p
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());

            app.UseRouting();
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
            }
        }
    }
}