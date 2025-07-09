using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers;

public class TodoController : Controller
{
    public IActionResult Index()
    {
        var todo = new Todo() { Id = 1, Task = "Clean room.", IsCompleted = false };
        return View(todo);
    }
}
