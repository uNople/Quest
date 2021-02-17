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
            await _todoDbContext.Add(new Todo
            {
                Description = description,
                Title = title,
            });
        }

        public async Task Delete(Todo todo)
        {
            await _todoDbContext.Delete(todo);
        }

        internal async Task SaveChanges()
        {
            await _todoDbContext.SaveChangesAsync();
        }
    }
}
