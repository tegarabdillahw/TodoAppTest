using Microsoft.EntityFrameworkCore;

using ToDoApp.Models;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Todos).WithOne(t => t.User);
        }
    }
}
