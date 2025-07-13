using System.ComponentModel.DataAnnotations;

namespace MyTodoApp.Models;

public class Todo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Task is required.")]
    public required string Task { get; set; }

    public bool IsCompleted { get; set; }
}
