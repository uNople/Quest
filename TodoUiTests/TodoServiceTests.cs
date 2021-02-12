using System;
using Xunit;
using TodoUi.Data;
using FluentAssertions;
using System.Linq;
using TodoUi.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using NSubstitute;

namespace TodoUiTests
{
    public class TodoServiceTests
    {
        private class DbTestDouble : ITodoDbContext
        {
            public List<Todo> Todos { get; set;} = new List<Todo>();
            public async Task Add(Todo todo)
            {
                await Task.Run(() => Todos.Add(todo));
            }

            public async Task Delete(Todo todo)
            {
                await Task.Run(() => Todos.Remove(todo));
            }

            public async Task<List<Todo>> Get()
            {
                return await Task.Run(() => Todos);
            }

            public void Migrate()
            {
                
            }
        }

        [Fact]
        public async Task When_adding_a_todo_Then_todo_exists_in_the_collection()
        {
            var dbTestDoble = Substitute.For<ITodoDbContext>();
            var service = new TodoService(dbTestDoble);
            await service.Create("", "");
            await dbTestDoble.Received(1).Add(Arg.Any<Todo>());
        }

        [Fact]
        public async Task When_deleting_a_todo_Then_todo_no_longer_exists_in_the_collection()
        {
            var dbTestDoble = Substitute.For<ITodoDbContext>();
            var service = new TodoService(dbTestDoble);
            await service.Create("", "");
            await dbTestDoble.Received(1).Add(Arg.Any<Todo>());
            var todo = new Todo();
            await service.Delete(todo);
            await dbTestDoble.Received(1).Delete(Arg.Any<Todo>());
        }
    }
}
