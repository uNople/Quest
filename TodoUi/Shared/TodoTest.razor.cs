using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        private List<Todo> Todos { get; set; } = new List<Todo>();
        private bool IsSaveHappening { get; set; }
        private string SaveDataButtonText { get; set; }

        private void SaveFromEvent()
        {
            Task.Run(async () => await InvokeAsync(async () => 
            {
                await SaveData();
                // TODO: work out what the threshold for needing to call this is
                // It seems like it works fine up until a certain number of changes per second
                StateHasChanged();
                await GetTodos();
                StateHasChanged();
            }));
        }

        private class SaveText
        {
            public const string Save = "Save";
            public const string Saving = "Saving";
            public const string Saved = "✓";
        }

        private async Task CreateTodo()
        {
            await DoSaveAction(
                async () =>
                {
                    await TodoService.Create(Title, Description);
                    await GetTodos();
                    Title = "";
                    Description = "";
                });
        }

        private async Task GetTodos()
        {
            Todos.ForEach(x => x.OnChanged -= SaveFromEvent);
            Todos = await TodoService.Get();
            Todos.ForEach(x => x.OnChanged += SaveFromEvent);
        }

        private async Task Delete(Todo todo)
        {
            await DoSaveAction(
                async () =>
                {
                    await TodoService.Delete(todo);
                    await GetTodos();
                });
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTodos();
            SaveDataButtonText = SaveText.Save;
            this.StateHasChanged();
        }

        private async Task SaveData()
        {
            await DoSaveAction(TodoService.SaveChanges);
        }

        private async Task DoSaveAction(Func<Task> action)
        {
            IsSaveHappening = true;
            SaveDataButtonText = SaveText.Saving;
            try
            {
                await action();
                await Task.Delay(250);
                SaveDataButtonText = SaveText.Saved;
                // TODO: Work out why we need this to update the UI. See background thread comment too
                StateHasChanged();
                await Task.Delay(250);
            }
            finally
            {
                SaveDataButtonText = SaveText.Save;
                IsSaveHappening = false;
            }
        }
    }
}
