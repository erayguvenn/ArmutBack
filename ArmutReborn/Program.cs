using ArmutReborn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Net;

namespace ArmutReborn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string remoteConnectionString = "server=34.127.86.147;uid=armut;pwd=eray;database=Armut";
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Events.OnRedirectToLogin = context =>
                  {
                      context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                      return Task.CompletedTask;
                  };
                  options.Cookie.Name = "session";
                  options.Cookie.HttpOnly = true;
                  options.Cookie.IsEssential = true;
                  options.Cookie.SameSite = SameSiteMode.None;
                  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
              });

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.ConsentCookie.IsEssential = true;
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            var serverVersion = new MariaDbServerVersion(new Version(10, 1, 48));
            builder.Services.AddDbContext<ArmutContext>(dbContextOptions => dbContextOptions
                .UseMySql(remoteConnectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors());


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            app.UseCors("MyPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}