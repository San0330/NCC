using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers;

public class TodoController : Controller
{
    public IActionResult Index()
    {
        var item = new Item() { Id = 1, Text = "Clean room.", IsCompleted = false };
        return View(item);
    }
}
