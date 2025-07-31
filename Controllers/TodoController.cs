using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTodoApp.Data;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var todos = await _context.Todos.ToListAsync();
        return View(todos);
    }

    // Show create form
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Todo todo)
    {
        if (!ModelState.IsValid)
            return View(todo);

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Show edit form
    public async Task<IActionResult> Edit(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
            return NotFound();

        return View(todo);
    }

    // Update
    [HttpPost]
    public async Task<IActionResult> Edit(Todo todo)
    {
        if (!ModelState.IsValid)
            return View(todo);

        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // Delete
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo != null)
        {
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
