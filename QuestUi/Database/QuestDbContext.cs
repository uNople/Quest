using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestUi.Data;

namespace QuestUi.Database
{
    public class QuestDbContext : DbContext, IQuestDbContext
    {
        public QuestDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Quest> Quests { get; set; }

        public async Task<List<Quest>> Get() => await Quests.ToListAsync();
        public async Task Add(Quest quest)
        {
            await Quests.AddAsync(quest);
            await SaveChangesAsync();
        }

        public async Task Delete(Quest quest)
        {
            Quests.Remove(quest);
            await SaveChangesAsync();
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public async Task SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
