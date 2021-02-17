using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TodoUi.Data;

namespace TodoUi.Database
{
    public interface ITodoDbContext : IDisposable
    {
        Task<List<Todo>> Get();
        Task Add(Todo todo);
        Task Delete(Todo todo);
        void Migrate();
        Task SaveChangesAsync();
    }
}