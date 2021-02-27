using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using QuestUi.Data;
using QuestUi.Database;
using Xunit;

namespace QuestUiTests
{
    public class QuestServiceTestsUsingTestDouble
    {
        private class DbTestDouble : IQuestDbContext
        {
            public List<Quest> Quests { get; set;} = new List<Quest>();
            public async Task Add(Quest quest)
            {
                await Task.Run(() => Quests.Add(quest));
            }

            public async Task Delete(Quest quest)
            {
                await Task.Run(() => Quests.Remove(quest));
            }

            public void Dispose()
            {
            }

            public async Task<List<Quest>> Get()
            {
                return await Task.Run(() => Quests);
            }

            public void Migrate()
            {
                
            }

            public Task SaveChangesAsync()
            {
                return Task.CompletedTask;
            }
        }

        [Fact]
        public async Task When_adding_a_quest_Then_dbContext_is_called_to_add_it()
        {
            var dbTestDoble = new DbTestDouble();
            var service = new QuestService(dbTestDoble);
            await service.Create("title", "description", Priority.Low);
            dbTestDoble.Quests.Count.Should().Be(1);
            dbTestDoble.Quests.First().Should().BeEquivalentTo(new Quest() {Title = "title", Description = "description"});
        }

        [Fact]
        public async Task When_deleting_a_quest_Then_dbContext_is_called_to_delete_it()
        {
            var dbTestDoble = new DbTestDouble();
            var service = new QuestService(dbTestDoble);
            await service.Create("", "", Priority.Low);
            dbTestDoble.Quests.Count.Should().Be(1);
            var quest = dbTestDoble.Quests.First();
            await service.Delete(quest);
            dbTestDoble.Quests.Count.Should().Be(0);
        }
    }
}
