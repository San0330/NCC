using Microsoft.EntityFrameworkCore;
using MyTodoApp.Models;

namespace MyTodoApp.Data;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
