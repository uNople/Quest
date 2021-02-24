using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using QuestUi.Database;

namespace QuestUi.Data
{
    public class QuestService
    {
        private readonly IQuestDbContext _questDbContext;
        public QuestService([NotNull]IQuestDbContext questDbContext)
        {
            _questDbContext = questDbContext;
        }

        public async Task<List<Quest>> Get() => await _questDbContext.Get();

        public async Task Create(string title, string description)
        {
            await _questDbContext.Add(new Quest
            {
                Description = description,
                Title = title,
            });
        }

        public async Task Delete(Quest quest)
        {
            await _questDbContext.Delete(quest);
        }

        internal async Task SaveChanges()
        {
            await _questDbContext.SaveChangesAsync();
        }
    }
}
