using System;
using Xunit;
using TodoUi.Data;
using FluentAssertions;
using System.Linq;

namespace TodoUiTests
{
    public class TodoServiceTests
    {
        [Fact]
        public void When_adding_a_todo_Then_todo_exists_in_the_collection()
        {
            var service = new TodoService();
            service.CreateTodo();
            service.Todos.Count.Should().Be(1);
        }

        [Fact]
        public void When_deleting_a_todo_Then_todo_no_longer_exists_in_the_collection()
        {
            var service = new TodoService();
            service.CreateTodo();
            service.Todos.Count.Should().Be(1);
            var todo = service.Todos.First();
            service.Delete(todo);
            service.Todos.Should().BeEmpty();
        }
    }
}
