using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using QuestUi.Data;

namespace QuestUi.Shared
{
    public partial class QuestLayout
    {
        [Inject]
        public QuestService QuestService { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private List<Quest> Quests { get; set; } = new List<Quest>();
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
                await GetQuests();
                StateHasChanged();
            }));
        }

        private class SaveText
        {
            public const string Save = "Save";
            public const string Saving = "Saving";
            public const string Saved = "✓";
        }

        private async Task CreateQuest()
        {
            await DoSaveAction(
                async () =>
                {
                    await QuestService.Create(Title, Description);
                    await GetQuests();
                    Title = "";
                    Description = "";
                });
        }

        private async Task GetQuests()
        {
            Quests.ForEach(x => x.OnChanged -= SaveFromEvent);
            Quests = await QuestService.Get();
            Quests.ForEach(x => x.OnChanged += SaveFromEvent);
        }

        private async Task Delete(Quest quest)
        {
            await DoSaveAction(
                async () =>
                {
                    await QuestService.Delete(quest);
                    await GetQuests();
                });
        }

        protected override async Task OnInitializedAsync()
        {
            await GetQuests();
            SaveDataButtonText = SaveText.Save;
            this.StateHasChanged();
        }

        private async Task SaveData()
        {
            await DoSaveAction(QuestService.SaveChanges);
        }

        private async Task DoSaveAction(Func<Task> action)
        {
            IsSaveHappening = true;
            SaveDataButtonText = SaveText.Saving;
            StateHasChanged();
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
