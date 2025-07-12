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
}
