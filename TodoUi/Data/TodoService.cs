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
        private readonly TodoDbContext _todoDbContext;

        public TodoService([NotNull]TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task Create(string title, string description)
        {
            _todoDbContext.Todos.Add(new Todo
            {
                Description = description,
                Title = title,
            });
            await _todoDbContext.SaveChangesAsync();
        }

        public async Task Delete(Todo todo)
        {
            _todoDbContext.Todos.Remove(todo);
            await _todoDbContext.SaveChangesAsync();
        }
    }
}
