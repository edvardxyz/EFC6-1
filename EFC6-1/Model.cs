using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EFC6_1
{

    public class ProjektManagerContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamWorker> TeamWorkers { get; set; } 

        public string DbPath { get; }

        public ProjektManagerContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamWorker>().HasKey(p => new { p.TeamId, p.WorkerId });
        }
    }

    public class Task
    {
        public int TaskId { get; set; }
        public string? Name { get; set; }
        public List<Todo> Todos { get; } = new();
    }
    public class Todo
    {
        public int TodoId { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
        public int TaskId { get; set; }
        public Task? Task { get; set; }
    }

    public class Team
    {
        public int TeamId { get; set; }
        public string? Name { get; set; }
        public List<Worker>? Workers { get; set; }
        public Task? CurrentTask { get; set; }
    }
    public class Worker
    {
        public int WorkerId { get; set; }
        public string? Name { get; set; }
        public List<Team>? Teams { get; set; }
        public Todo? CurrentTodo { get; set; }

    }
    public class TeamWorker
    {
        public int TeamId { get; set; }
        public Team? Team { get; set; }
        public Worker? Worker { get; set; }
        public int WorkerId { get; set; }
    }

}
