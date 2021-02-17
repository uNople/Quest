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
    public class TodoServiceTestsUsingMock
    {
        [Fact]
        public async Task When_adding_a_todo_Then_dbContext_is_called_to_add_it()
        {
            var todoDbContext = Substitute.For<ITodoDbContext>();
            var service = new TodoService(todoDbContext);
            await service.Create("title", "description");
            await todoDbContext.Received(1).Add(Arg.Is<Todo>(x => x.Description == "description" && x.Title == "title"));
        }

        [Fact]
        public async Task When_deleting_a_todo_Then_dbContext_is_called_to_delete_it()
        {
            var todoDbContext = Substitute.For<ITodoDbContext>();
            var service = new TodoService(todoDbContext);
            await service.Create("", "");
            await todoDbContext.Received(1).Add(Arg.Any<Todo>());
            var todo = new Todo();
            await service.Delete(todo);
            await todoDbContext.Received(1).Delete(Arg.Any<Todo>());
        }
    }
}
