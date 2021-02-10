using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using TodoUi.Database;

namespace TodoUi.Data
{
    public class TodoService
    {
        private readonly ITodoDbContext _todoDbContext;
        public TodoService([NotNull]ITodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<List<Todo>> Get() => await _todoDbContext.Get();

        public async Task Create(string title, string description)
        {
            Todos.Add(new Todo
            {
                Description = description,
                Title = title,
            });
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task Delete(Todo todo)
        {
            Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();
        }
    }
}
