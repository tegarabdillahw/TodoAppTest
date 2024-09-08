using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using TodoApp.Data;
using Microsoft.EntityFrameworkCore;

using ToDoApp.Services;
using TodoApp.Services;

namespace TodoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Register the To-Do services (UserService, ToDoService)
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TodoService>();  // Ensure service name matches TodoService.cs

            // Use file-based SQLite for persistence
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("DataSource=app.db"));  // Use a file-based SQLite database

            var app = builder.Build();

            // Initialize and ensure SQLite file-based database is created
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.EnsureCreated();  // Ensure the schema is created
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
