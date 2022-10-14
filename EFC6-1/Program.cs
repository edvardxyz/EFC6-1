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
                // Task 1
                Task task1 = new Task { Name = "Produce software" };
                Team teamf = new Team { Name = "Frontend", CurrentTask = task1};
                context.Add(task1);
                Todo todo1_1 = new Todo { Name = "Write code", IsComplete = false };
                Todo todo1_2 = new Todo { Name = "Compile source", IsComplete = false };
                Todo todo1_3 = new Todo { Name = "Test program", IsComplete = false };

                task1.Todos.Add(todo1_1);
                task1.Todos.Add(todo1_2);
                task1.Todos.Add(todo1_3);


                AddWorker(context, teamf, "Steen Secher", todo1_1);
                AddWorker(context, teamf, "Ejvind Møller", todo1_2);
                AddWorker(context, teamf, "Konrad Sommer", todo1_3);

                // Task 2
                Task task2 = new Task { Name = "Brew coffee"};
                Team teamb = new Team { Name = "Backend", CurrentTask = task2 };
                context.Add(task2);
                Todo todo2_1 = new Todo { Name = "Pour water", IsComplete = false };
                Todo todo2_2 = new Todo { Name = "Pour coffee", IsComplete = false };
                Todo todo2_3 = new Todo { Name = "Turn on", IsComplete = false };
                task2.Todos.Add(todo2_1);
                task2.Todos.Add(todo2_2);
                task2.Todos.Add(todo2_3);

                AddWorker(context, teamb, "Konrad Sommer", todo2_1);
                AddWorker(context, teamb, "Sofus Lotus", todo2_2);
                AddWorker(context, teamb, "Remo Lademann", todo2_3);

                // Task 3
                Task task3 = new Task { Name = "Create ORM" };
                Team teamt = new Team { Name = "Testere", CurrentTask = task3 };
                Todo todo3_1 = new Todo { Name = "Code code", IsComplete = false };
                Todo todo3_2 = new Todo { Name = "Compile the code", IsComplete = false };
                Todo todo3_3 = new Todo { Name = "Test ORM", IsComplete = false };

                context.Add(task3);
                task3.Todos.AddRange(new List<Todo> { todo3_1, todo3_2, todo3_3 });

                AddWorker(context, teamt, "Ella Fanth", todo3_1);
                AddWorker(context, teamt, "Anne Dam", todo3_2);
                AddWorker(context, teamt, "Steen Secher", todo3_3);

                context.SaveChanges();
            }
        }
        private static void AddWorker(ProjektManagerContext context, Team team, string worker, Todo todo)
        {
            context.TeamWorkers.Add(new TeamWorker
            {
                Team = team,
                Worker = new Worker { Name = worker, CurrentTodo = todo }
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

