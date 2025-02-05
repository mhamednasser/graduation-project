using graduationProject.Interfaces;
using graduationProject.Models;
using graduationProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace graduationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register DbContext to use SQL Server with connection string from the configuration
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add Identity Services for user authentication and management
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders(); // For password reset functionality

            // Configure Authentication (e.g., Google Login) (Optional, if needed)
            builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "<Your-Google-Client-ID>";
                    options.ClientSecret = "<Your-Google-Client-Secret>";
                });

            // Add CORS (Cross-Origin Resource Sharing) to allow frontend communication (e.g., Flutter/Vue.js)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()   // Allows any origin (for local testing)
                           .AllowAnyMethod()   // Allows any HTTP method (GET, POST, PUT, DELETE)
                           .AllowAnyHeader();  // Allows any headers
                });
            });

            // Add HttpClient for making external requests (e.g., OpenAI integration)
            builder.Services.AddHttpClient<PlanGenerationService>(client =>
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["OpenAI:ApiKey"]}");
            });

            // HttpClient for PythonApiService
            builder.Services.AddHttpClient<PythonApiService>();
            // Add Scoped services for other services (if any)
            builder.Services.AddScoped<IPlanGenerationService, PlanGenerationMockService>();   // PlanGenerationMockService           PlanGenerationService

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();  // Enable Swagger UI for API testing and documentation
            }

            // Enable HTTPS Redirection
            app.UseHttpsRedirection();

            // Apply CORS policy to the app
            app.UseCors("AllowAll");

            // Add Authentication Middleware (to check tokens, cookies, etc.)
            app.UseAuthentication();

            // Add Authorization Middleware (to verify if the user has the right permissions)
            app.UseAuthorization();

            // Map controllers to handle API requests
            app.MapControllers();

            // Start the application
            app.Run();
        }
    }
}
