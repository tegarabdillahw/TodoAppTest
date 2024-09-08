

namespace ToDoApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Todo> Todos { get; set; }
    }
}
