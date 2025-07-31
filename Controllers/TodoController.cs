using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyTodoApp.Models;

namespace MyTodoApp.Controllers;

public class TodoController : Controller
{
    private readonly string _connectionString =
        "Server=localhost;Database=mytodoapp;User ID=santosh;Password=password;";

    public IActionResult Index()
    {
        List<Todo> todos = new List<Todo>();

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "SELECT * FROM Todos";
        using var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(
                new Todo
                {
                    Id = reader.GetInt32("Id"),
                    Task = reader.GetString("Task"),
                    IsCompleted = reader.GetBoolean("IsCompleted"),
                }
            );
        }
        return View(todos);
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

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query =
            @"INSERT INTO 
                        Todos (Task, IsCompleted) 
                        VALUES (@task, @isCompleted)";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@task", todo.Task);
        cmd.Parameters.AddWithValue("@isCompleted", false);
        cmd.ExecuteNonQuery();

        return RedirectToAction("Index");
    }

    // Show edit form
    public IActionResult Edit(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "SELECT * FROM Todos WHERE Id = @id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            var todo = new Todo
            {
                Id = reader.GetInt32("Id"),
                Task = reader.GetString("Task"),
                IsCompleted = reader.GetBoolean("IsCompleted"),
            };
            return View(todo);
        }

        return NotFound();
    }

    // Update
    [HttpPost]
    public IActionResult Edit(Todo updatedTodo)
    {
        if (!ModelState.IsValid)
            return View(updatedTodo);

        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query =
            @"UPDATE Todos 
                 SET Task = @task, 
                     IsCompleted = @isCompleted 
                 WHERE Id = @id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@task", updatedTodo.Task);
        cmd.Parameters.AddWithValue("@isCompleted", updatedTodo.IsCompleted);
        cmd.Parameters.AddWithValue("@id", updatedTodo.Id);
        cmd.ExecuteNonQuery();

        return RedirectToAction("Index");
    }

    // Delete
    [HttpPost]
    public IActionResult Delete(int id)
    {
        using var conn = new MySqlConnection(_connectionString);
        conn.Open();

        string query = "DELETE FROM Todos WHERE Id = @id";
        using var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();

        return RedirectToAction("Index");
    }
}
