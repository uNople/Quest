using System;
using Xunit;
using TodoUi.Data;
using FluentAssertions;

namespace TodoUiTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var service = new TodoService();
            service.AddTask();
            service.Tasks.Count.Should().Be(1);
        }
    }
}
