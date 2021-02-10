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
            var dbTestDoble = new DbTestDouble();
            var service = new TodoService(dbTestDoble);
            await service.Create("", "");
            dbTestDoble.Todos.Count().Should().Be(1);
        }

        [Fact]
        public async Task When_deleting_a_todo_Then_todo_no_longer_exists_in_the_collection()
        {
            var dbTestDoble = new DbTestDouble();
            var service = new TodoService(dbTestDoble);
            await service.Create("", "");
            dbTestDoble.Todos.Count().Should().Be(1);
            var todo = dbTestDoble.Todos.First();
            await service.Delete(todo);
            dbTestDoble.Todos.Should().BeEmpty();
        }
    }
}
