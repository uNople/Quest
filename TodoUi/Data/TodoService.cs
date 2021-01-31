using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;

namespace TodoUi.Data
{
    public class TodoService
    {
        public List<Todo> Tasks { get; set; } = new List<Todo>();
        private static Random _rng = new Random();

        public void UpdateHeading(MouseEventArgs e, Todo todo)
        {
            Delete(todo);
        }

        public void AddTask()
        {
            AddTask($"Task {_rng.Next()}", $"Description {_rng.Next()}", RandomBool());
        }

        private bool RandomBool()
        {
            return _rng.Next(0, 2) == 0 ? false : true;
        }
        private void AddTask(string title, string description, bool isCompleted)
        {
            Tasks.Add(new Todo
            {
                Description = description,
                Title = title,
                IsCompleted = isCompleted,
            });
        }

        private void Delete(Todo todo)
        {
            Tasks.Remove(todo);
        }
    }
}
