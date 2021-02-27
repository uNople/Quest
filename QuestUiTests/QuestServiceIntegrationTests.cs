using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuestUi.Data;
using QuestUi.Database;
using Xunit;

namespace QuestUiTests
{
    public class QuestServiceIntegrationTests
    {
        [Fact]
        public async Task When_adding_a_quest_Then_it_exists_in_the_database()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuestDbContext>()
                .UseInMemoryDatabase("InMemoryDb");

            using (var db = new QuestDbContext(optionsBuilder.Options))
            {
                var quest = new Quest() { Title = "test", Description = "best", Priority = Priority.High };
                await db.Add(quest);
                var shouldExist = (await db.Get()).First();
                shouldExist.Should().BeEquivalentTo(quest);
                shouldExist.Id.Should().Be(1);

                quest = new Quest() { Title = "test2", Description = "best2", Priority = Priority.High };
                await db.Add(quest);
                shouldExist = (await db.Get()).Skip(1).First();
                shouldExist.Should().BeEquivalentTo(quest);
                shouldExist.Id.Should().Be(2);

                await db.Delete(quest);
                shouldExist = (await db.Get()).Skip(1).FirstOrDefault();
                shouldExist.Should().BeNull();

                quest = new Quest() { Title = "test3", Description = "best3", Priority = Priority.High };
                await db.Add(quest);
                shouldExist = (await db.Get()).Skip(1).First();
                shouldExist.Should().BeEquivalentTo(quest);
                shouldExist.Id.Should().Be(3);
            }
        }

        [Fact]
        public async Task When_using_quest_service_Then_it_exists_in_the_database()
        {
            var optionsBuilder = new DbContextOptionsBuilder<QuestDbContext>()
                .UseInMemoryDatabase("InMemoryDb2");

            using (var db = new QuestDbContext(optionsBuilder.Options))
            {
                var service = new QuestService(db);
                await service.Create("test", "best", Priority.High);
                var quest = new Quest() { Title = "test", Description = "best", Id = 1, Priority = Priority.High };
                var questFromDb = (await service.Get()).First();
                questFromDb.Should().BeEquivalentTo(quest);
                
                await service.Create("test2", "best2", Priority.High);
                var quest2 = new Quest() { Title = "test2", Description = "best2", Id = 2, Priority = Priority.High };
                var quest2FromDb = (await service.Get()).Skip(1).First();
                quest2FromDb.Should().BeEquivalentTo(quest2);

                await service.Delete(quest2FromDb);
                var deletedQuest2 = (await service.Get()).Skip(1).FirstOrDefault();
                deletedQuest2.Should().BeNull();
                
                await service.Create("test3", "best3", Priority.High);
                var quest3 = new Quest() { Title = "test3", Description = "best3", Id = 3, Priority = Priority.High};
                var quest3FromDb = (await service.Get()).Skip(1).First();
                quest3FromDb.Should().BeEquivalentTo(quest3);
            }
        }

        [Fact]
        public void When_creating_the_app_Then_it_should_not_error()
        {

            var host = QuestUi.Program.CreateHostBuilder(new string[0]).Build();
            using (var scope = host.Services.CreateScope())
            {
                Action act = () => scope.ServiceProvider.GetService<IQuestDbContext>();
                act.Should().NotThrow();
            }
        }

        [Fact]
        public void When_creating_the_app_Then_we_can_resolve_quest_service()
        {
            var host = QuestUi.Program.CreateHostBuilder(new string[0]).Build();
            using (var scope = host.Services.CreateScope())
            {
                Action act = () => scope.ServiceProvider.GetService<QuestService>();
                act.Should().NotThrow();
            }
        }
    }
}
