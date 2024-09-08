using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public class TodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateTodo(int userId, string subject, string description)
        {
            var activityNo = $"AC-{_context.Todos.Count() + 1:D4}";

            var todo = new Todo
            {
                UserId = userId,
                Subject = subject,
                Description = description,
                ActivityNo = activityNo,
                Status = TodoStatus.Unmarked
            };

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetUserTodos(int userId)
        {
            return await _context.Todos.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task MarkTodoStatus(int todoId, TodoStatus status)
        {
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo != null)
            {
                todo.Status = status;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTodoIfUnmarked(int todoId)
        {
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo != null && todo.Status == TodoStatus.Unmarked)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTodo(int todoId, string subject, string description)
        {
            var todo = await _context.Todos.FindAsync(todoId);
            if (todo != null && todo.Status == TodoStatus.Unmarked)
            {
                todo.Subject = subject;
                todo.Description = description;
                await _context.SaveChangesAsync();
            }
        }
    }
}
