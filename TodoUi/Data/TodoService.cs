using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace TodoUi.Data
{
    public class TodoService
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();

        public void Create(string title, string description)
        {
            Todos.Add(new Todo
            {
                Description = description,
                Title = title,
            });
        }

        public void Delete(Todo todo)
        {
            Todos.Remove(todo);
        }
    }
}
