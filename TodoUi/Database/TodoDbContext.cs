using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoUi.Data;

namespace TodoUi.Database
{
    public class TodoDbContext : DbContext, ITodoDbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Todo> Todos { get; set; }

        public async Task<List<Todo>> Get() => await Todos.ToListAsync();
        public async Task Add(Todo todo)
        {
            await Todos.AddAsync(todo);
            await SaveChangesAsync();
        }

        public async Task Delete(Todo todo)
        {
            Todos.Remove(todo);
            await SaveChangesAsync();
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}
