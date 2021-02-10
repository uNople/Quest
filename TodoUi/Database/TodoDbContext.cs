using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using TodoUi.Data;

namespace TodoUi.Database
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
