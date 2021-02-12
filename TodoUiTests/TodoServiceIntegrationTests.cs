using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoUi.Data;
using TodoUi.Database;
using Xunit;

namespace TodoUiTests
{
    public class TodoServiceIntegrationTests
    {
        [Fact]
        public async Task When_adding_a_todo_Then_it_exists_in_the_database()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("InMemoryDb");

            using (var db = new TodoDbContext(optionsBuilder.Options))
            {
                var todo = new Todo() { Title = "test", Description = "best" };
                await db.Add(todo);
                var shouldExist = (await db.Get()).First();
                shouldExist.Should().BeEquivalentTo(todo);
                shouldExist.Id.Should().Be(1);

                todo = new Todo() { Title = "test2", Description = "best2" };
                await db.Add(todo);
                shouldExist = (await db.Get()).Skip(1).First();
                shouldExist.Should().BeEquivalentTo(todo);
                shouldExist.Id.Should().Be(2);

                await db.Delete(todo);
                shouldExist = (await db.Get()).Skip(1).FirstOrDefault();
                shouldExist.Should().BeNull();

                todo = new Todo() { Title = "test3", Description = "best3" };
                await db.Add(todo);
                shouldExist = (await db.Get()).Skip(1).First();
                shouldExist.Should().BeEquivalentTo(todo);
                shouldExist.Id.Should().Be(3);
            }
        }

        [Fact]
        public async Task When_using_todo_service_Then_it_exists_in_the_database()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase("InMemoryDb2");

            using (var db = new TodoDbContext(optionsBuilder.Options))
            {
                var service = new TodoService(db);
                await service.Create("test", "best");
                var todo = new Todo() { Title = "test", Description = "best", Id = 1 };
                var todoFromDb = (await service.Get()).First();
                todoFromDb.Should().BeEquivalentTo(todo);
                
                await service.Create("test2", "best2");
                var todo2 = new Todo() { Title = "test2", Description = "best2", Id = 2 };
                var todo2FromDb = (await service.Get()).Skip(1).First();
                todo2FromDb.Should().BeEquivalentTo(todo2);

                await service.Delete(todo2FromDb);
                var deletedTodo2 = (await service.Get()).Skip(1).FirstOrDefault();
                deletedTodo2.Should().BeNull();
                
                await service.Create("test3", "best3");
                var todo3 = new Todo() { Title = "test3", Description = "best3", Id = 3};
                var todo3FromDb = (await service.Get()).Skip(1).First();
                todo3FromDb.Should().BeEquivalentTo(todo3);
            }
        }
    }
}
