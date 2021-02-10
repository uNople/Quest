using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoUi.Data;
using TodoUi.Database;

namespace TodoUi.Shared
{
    public partial class TodoTest
    {
        [Inject]
        public TodoService TodoService { get; set; }
        [Inject]
        public TodoDbContext TodoDbContext { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }

        private async Task CreateTodo()
        {
            await TodoService.Create(Title, Description);
            Title = "";
            Description = "";
        }
    }
}
