using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskSistem.Data;
using TaskSistem.Repositories;
using TaskSistem.Repositories.Interfaces;
using TaskSistem.Services;
using TaskSistem.Services.Interfaces;

namespace TaskSistem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string jwtSecret = builder.Configuration["JwtSettings:Secret"]
                ?? throw new Exception("JWT Secret não configurada.");

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<TaskSistemDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
            );

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                    ValidateIssuer = true, // Mude para true se tiver um Issuer definido
                    ValidateAudience = true, // Mude para true se tiver um Audience definido
                    ValidateLifetime = true,
                    ValidIssuer = "devv",
                    ValidAudience = "devv"
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TaskSistemDBContext>();
                    context.Database.Migrate();
                    Console.WriteLine("Banco de dados inicializado e migrado com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao rodar as migrations: {ex.Message}");
                }
            }

            app.Run();
        }
    }
}
