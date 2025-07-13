using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers;

public class TodoController : Controller
{
    private static List<Todo> _todos = new List<Todo>
    {
        new Todo
        {
            Id = 1,
            Task = "Learn ASP.NET MVC",
            IsCompleted = false,
        },
        new Todo
        {
            Id = 2,
            Task = "Build a Todo App",
            IsCompleted = true,
        },
    };

    public IActionResult Index()
    {
        return View(_todos);
    }

    // Show create form
    public IActionResult Create()
    {
        return View();
    }

    // Create
    [HttpPost]
    public IActionResult Create(Todo todo)
    {
        todo.Id = _todos.Count + 1; // Simple ID generation
        todo.IsCompleted = false; // Default to not completed
        _todos.Add(todo);
        return RedirectToAction("Index");
    }
}
