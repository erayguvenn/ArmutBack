using ArmutReborn.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace ArmutReborn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string remoteConnectionString = "server=34.127.86.147;uid=armut;pwd=eray;database=Armut";
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.Name = "session_id";
              });

            builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                        
            }));

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

         
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("MyPolicy");
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}