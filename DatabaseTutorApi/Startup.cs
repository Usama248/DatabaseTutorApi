using AutoMapper;
using CommonLayer;
using CommonLayer.Helper;
using DatabaseTutorApi.API.Utilities;
using DatabaseTutorApi.Buisness.DIHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.DataSeeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.DIHelper;
using static CommonLayer.Constants;
using static EntityLayer.Helpers.ConnectionStringHelper;

namespace DatabaseTutorApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;
            Utils._config = new ConfigurationBuilder().SetBasePath(_env.ContentRootPath).AddJsonFile("appSettings.json").Build();
            ConnectionStrings.DatabaseTutorConnectionString = Utils._config["ConnectionStrings:DatabaseTutorConnectionString"];
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.RegisterCustomServices();

            services.AddHttpContextAccessor();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build();
            });

            services.AddAuthentication()
             .AddCookie()
             .AddJwtBearer(config =>
             {
                 config.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidIssuer = JWTConfiguration.JWTIssuer,
                     ValidAudience = JWTConfiguration.JWTAudience,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTConfiguration.JWTKey)),
                     ClockSkew = TimeSpan.Zero,
                     //LifetimeValidator = TokenLifetimeValidator.Validate
                 };
             });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mappingConfig.CreateMapper());

            if (_env.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DataBase Tutor CRM API", Version = "v1" });

                    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        In = ParameterLocation.Header,
                        Description = "Basic Authorization header using the Bearer scheme."
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                      {
                          {
                                new OpenApiSecurityScheme
                                  {
                                      Reference = new OpenApiReference
                                      {
                                          Type = ReferenceType.SecurityScheme,
                                          Id = "basic"
                                      }
                                  },
                                  new string[] {}
                          }
                      });
                });
            }
        }
 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Seeder seeder)
        {
            ServiceActivator.Configure(app.ApplicationServices);

            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(SwaggerConfiguration.SwaggerEndPointURL, SwaggerConfiguration.SwaggerEndPointName);
                });
            }
            else
            {
                app.UseHsts();
            }

            seeder.Seed().Wait();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            List<string> origins = new List<string> { "http://localhost:4200", "https://localhost:4200", "http://localhost:4300", "http://localhost:4500" };

            app.UseCors(options =>
            {
                options.WithOrigins(origins.ToArray()).AllowAnyMethod().AllowCredentials().AllowAnyHeader().SetIsOriginAllowed((host) => true);
            });

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
