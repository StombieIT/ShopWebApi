using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopWebApi.Models;
using System;
using System.Text;
using MediatR;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace ShopWebApi
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => builder    
                .WithOrigins(configuration.GetSection("AllowOrigins").Get<string[]>())
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );
            });
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ShopWebApiDB")));
            services.AddScoped<IRepository, EFRepository>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
                    options.Cookie.SameSite = SameSiteMode.None;
                    options.LoginPath = "/Account/SignIn";
                    options.ReturnUrlParameter = "History";
                })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration.GetSection("Secret").Get<string>())
                        ),
                        ValidIssuers = configuration.GetSection("Issuers").Get<string[]>(),
                        ValidAudiences = configuration.GetSection("Audiences").Get<string[]>(),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Swagger Demo API",
                    Description = "Api for working with shop",
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Demo API");
                options.RoutePrefix = "";
            });
        }
    }
}
