using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPIOrderTracking.Guards.Hashers;
using WebAPIOrderTracking.Guards.Interfaces;
using WebAPIOrderTracking.Guards.Logic;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<OrderTrackingContext>(options =>
            {
                options.UseSqlServer("name=Connection");
            });
            builder.Services.AddTransient<IAuthLogic, JwtAuthLogic>();
            builder.Services.AddTransient<IHasherable, SecretHasher>();
            builder.Services.AddAuthentication(opt => {
              opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://www.ordertracking.somee.com",
                ValidAudience = "https://www.ordertracking.somee.com",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"))
              };
              });

            builder.Services.AddCors(options =>
            {
              options.AddPolicy("EnableCORS", builder =>
              {
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
              });
            });

            

            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseCors("EnableCORS");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }

            app.UseHttpsRedirection();

            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
    }
    }
}
