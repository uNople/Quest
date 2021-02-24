using System.Threading.Tasks;
using NSubstitute;
using QuestUi.Data;
using QuestUi.Database;
using Xunit;

namespace QuestUiTests
{
    public class QuestServiceTestsUsingMock
    {
        [Fact]
        public async Task When_adding_a_quest_Then_dbContext_is_called_to_add_it()
        {
            var questDbContext = Substitute.For<IQuestDbContext>();
            var service = new QuestService(questDbContext);
            await service.Create("title", "description");
            await questDbContext.Received(1).Add(Arg.Is<Quest>(x => x.Description == "description" && x.Title == "title"));
        }

        [Fact]
        public async Task When_deleting_a_todo_Then_dbContext_is_called_to_delete_it()
        {
            var questDbContext = Substitute.For<IQuestDbContext>();
            var service = new QuestService(questDbContext);
            await service.Create("", "");
            await questDbContext.Received(1).Add(Arg.Any<Quest>());
            var quest = new Quest();
            await service.Delete(quest);
            await questDbContext.Received(1).Delete(Arg.Any<Quest>());
        }
    }
}
