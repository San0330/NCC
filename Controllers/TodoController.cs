using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Models;
using MyTodoApp.Services;

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

    private static int _nextId = _todos.Count + 1;

    private readonly ILog _logger;

    public TodoController(ILog logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        _logger.Info("Accessed Todo Index");

        // Resolve the ILog implementation manually
        // var logger = HttpContext.RequestServices.GetRequiredService<ILog>();
        // logger.Info("Visited Index page using HttpContext.RequestServices");
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
        if (!ModelState.IsValid)
        {
            return View(todo); // Return with validation errors
        }
        todo.Id = _nextId++; // Simple ID generation
        todo.IsCompleted = false; // Default to not completed
        _todos.Add(todo);
        return RedirectToAction("Index");
    }

    // Show edit form
    public IActionResult Edit(int id)
    {
        var todo = _todos.FirstOrDefault(t => t.Id == id);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

    // Update
    [HttpPost]
    public IActionResult Edit(Todo updatedTodo)
    {
        if (!ModelState.IsValid)
        {
            return View(updatedTodo); // Return with validation errors
        }

        var todo = _todos.FirstOrDefault(t => t.Id == updatedTodo.Id);
        if (todo == null)
            return NotFound();

        todo.Task = updatedTodo.Task;
        todo.IsCompleted = updatedTodo.IsCompleted;
        return RedirectToAction("Index");
    }

    // Delete
    [HttpPost]
    public IActionResult Delete(int id)
    {
        _todos.RemoveAll(t => t.Id == id);
        return RedirectToAction("Index");
    }
}
