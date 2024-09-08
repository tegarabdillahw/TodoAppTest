using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

using BCrypt.Net;
using ToDoApp.Models;

namespace TodoApp.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(string userId, string password, string name)
        {
            if (await _context.Users.AnyAsync(u => u.UserId == userId))
                throw new Exception("User already exists.");

            var user = new User
            {
                UserId = userId,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Name = name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> Authenticate(string userId, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserId == userId);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("Invalid credentials.");

            return user;
        }
    }
}
