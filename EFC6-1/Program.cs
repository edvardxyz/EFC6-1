using EFC6_1.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
namespace EFC6_1
{
    public class Program
    {
        public static void Main(string[] Args)
        {

            //SeedTasks();
            SeedWorkers();
            //PrintAllTasks();
            //PrintIncompleteTasksAndTodos();
        }
        private static void SeedWorkers()
        {
            using (ProjektManagerContext context = new())
            {
                Team teamf = new Team { Name = "Frontend" };
                Team teamb = new Team { Name = "Backend" };
                Team teamt = new Team { Name = "Testere" };

                AddWorker(context, teamf, "Steen Secher");
                AddWorker(context, teamf, "Ejvind Møller");
                AddWorker(context, teamf, "Konrad Sommer");

                AddWorker(context, teamb, "Konrad Sommer");
                AddWorker(context, teamb, "Sofus Lotus");
                AddWorker(context, teamb, "Remo Lademann");

                AddWorker(context, teamt, "Ella Fanth");
                AddWorker(context, teamt, "Anne Dam");
                AddWorker(context, teamt, "Steen Secher");

                context.SaveChanges();
            }
        }
        private static void AddWorker(ProjektManagerContext context, Team team, string worker)
        {
            context.TeamWorkers.Add(new TeamWorker
            {
                Team = team,
                Worker = new Worker { Name = worker }
            });
        }
        private static void PrintIncompleteTasksAndTodos()

        {
            using (ProjektManagerContext context = new())
            {
                var tasks = context.Tasks.Include(task => task.Todos).Where(task => task.Todos.Any(todo => todo.IsComplete == false));
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task: {task.Name}");
                    foreach (var todo in task.Todos)
                    {
                        Console.WriteLine($"-- {todo.Name}");
                    }
                }
            }

        }
        private static void SeedTasks()
        {
            using var db = new ProjektManagerContext();

            Task task1 = new Task { Name = "Produce software" };
            db.Add(task1);

            task1.Todos.Add(new Todo { Name = "Write code", IsComplete = false });
            task1.Todos.Add(new Todo { Name = "Compile source", IsComplete = false });
            task1.Todos.Add(new Todo { Name = "Test program", IsComplete = false });

            Task task2 = new Task { Name = "Brew coffee" };
            db.Add(task2);
            task2.Todos.Add(new Todo { Name = "Pour water", IsComplete = false });
            task2.Todos.Add(new Todo { Name = "Pour coffee", IsComplete = false });
            task2.Todos.Add(new Todo { Name = "Turn on", IsComplete = false });

            db.SaveChanges();
        }
        // Opgave 2.1
        private static void PrintAllTasks()
        {
            using (ProjektManagerContext context = new())
            {
                var tasks = context.Tasks.Include(task => task.Todos);
                foreach (var task in tasks)
                {
                    Console.WriteLine($"Task: {task.Name}");
                    foreach (var todo in task.Todos)
                    {
                        Console.WriteLine($"-- {todo.Name}");
                    }
                }
            }
        }
    }
}

