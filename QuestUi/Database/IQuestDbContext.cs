using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuestUi.Data;

namespace QuestUi.Database
{
    public interface IQuestDbContext : IDisposable
    {
        Task<List<Quest>> Get();
        Task Add(Quest quest);
        Task Delete(Quest quest);
        void Migrate();
        Task SaveChangesAsync();
    }
}