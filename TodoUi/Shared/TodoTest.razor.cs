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
        private string Title { get; set; }
        private string Description { get; set; }
        private List<Todo> Todos { get; set;}

        private async Task CreateTodo()
        {
            await TodoService.Create(Title, Description);
            Todos = await TodoService.Get();
            Title = "";
            Description = "";
        }

        private async Task Delete(Todo todo)
        {
            await TodoService.Delete(todo);
            Todos = await TodoService.Get();
        }

        protected override async Task OnInitializedAsync()
        {
            Todos = await TodoService.Get();
            this.StateHasChanged();
        }
    }
}
