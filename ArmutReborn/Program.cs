using ArmutReborn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using System.Net;
using System.Text.Json.Serialization;

namespace ArmutReborn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string remoteConnectionString = "server=3.127.53.229;uid=Eray;pwd=armut;database=Armut";
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseUrls("http://localhost:5058","http://*:5058");
           /* builder.WebHost.UseUrls("https://localhost:7058", "http://localhost:5058", "https://*:7058", "http://*:5058");*/
            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:4200","*")
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

            builder.Services.AddControllers().AddJsonOptions(options => {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            Console.WriteLine(app.Urls);

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