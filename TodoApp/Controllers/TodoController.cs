using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(int userId, string subject, string description)
        {
            await _todoService.CreateTodo(userId, subject, description);
            return Ok("To-Do created successfully.");
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTodos(int userId)
        {
            var todos = await _todoService.GetUserTodos(userId);
            return Ok(todos);
        }

        [HttpPost("mark/{todoId}")]
        public async Task<IActionResult> Mark(int todoId, TodoStatus status)
        {
            await _todoService.MarkTodoStatus(todoId, status);
            return Ok("To-Do marked successfully.");
        }

        [HttpDelete("delete/{todoId}")]
        public async Task<IActionResult> Delete(int todoId)
        {
            await _todoService.DeleteTodoIfUnmarked(todoId);
            return Ok("To-Do deleted successfully.");
        }

        [HttpPut("update/{todoId}")]
        public async Task<IActionResult> Update(int todoId, string subject, string description)
        {
            await _todoService.UpdateTodo(todoId, subject, description);
            return Ok("To-Do updated successfully.");
        }
    }
}
