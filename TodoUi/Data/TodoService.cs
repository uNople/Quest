using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoUi.Data
{
    public class TodoService
    {
        public List<Todo> Tasks { get; set; } = new List<Todo>();
        
        public void AddTask(string title, string description)
        {
            Tasks.Add(new Todo
            {
                Description = description,
                Title = title
            });
        }
    }
}