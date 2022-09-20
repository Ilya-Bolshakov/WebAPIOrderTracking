using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPIOrderTracking.Models.Entities;

namespace WebAPIOrderTracking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

      // Add services to the container.
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
                ValidIssuer = "https://localhost:7195",
                ValidAudience = "https://localhost:7195",
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

            builder.Services.AddDbContext<OrderTrackingContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Connection"));
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
