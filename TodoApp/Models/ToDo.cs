namespace ToDoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string ActivityNo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public TodoStatus Status { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
