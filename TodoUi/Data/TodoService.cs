using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace TodoUi.Data
{
    public class TodoService
    {
        public List<Todo> Todos { get; set; } = new List<Todo>();
        private static Random _rng = new Random();

        public void UpdateHeading(MouseEventArgs e, Todo todo)
        {
            Delete(todo);
        }

        public void CreateTodo()
        {
            CreateTodo($"Task {_rng.Next()}", $"Description {_rng.Next()}", RandomBool());
        }

        private bool RandomBool()
        {
            return _rng.Next(0, 2) == 0 ? false : true;
        }

        private void CreateTodo(string title, string description, bool isCompleted)
        {
            Todos.Add(new Todo
            {
                Description = description,
                Title = title,
                IsCompleted = isCompleted,
            });
        }

        public void Delete(Todo todo)
        {
            Todos.Remove(todo);
        }
    }
}
