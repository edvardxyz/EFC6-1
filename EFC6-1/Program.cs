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
            //SeedWorkers();
            //SeedTeamWithoutTask();
            //PrintAllTasks();
            //PrintIncompleteTasksAndTodos();
            //PrintTeamsWithoutTasks();
            //PrintTeamCurrentTask();
            PrintTeamProgress();
        }
        private static void PrintIncompleteTasksAndTodos()
        {
            using (ProjektManagerContext context = new())
            {
                var tasks = context.Tasks.
                    Include(task => task.Todos).
                    Where(task => task.Todos.Any(todo => todo.IsComplete == false ));

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
        private static void PrintTeamProgress()
        {
            using (ProjektManagerContext db = new())
            {
                var tasks = db.Tasks.Include(t => t.Todos);
                var teams = db.Teams.Include(t => t.CurrentTask).Include(t => t.CurrentTask.Todos);

                foreach (var team in teams)
                {
                    if (team.CurrentTask == null)
                    {
                        Console.Write($"Team: {team.Name}: NO TASK!! :O");
                        continue;
                    }
                    Console.Write($"Team: {team.Name}: ");
                    float done_counter = 0;
                    float total = team.CurrentTask.Todos.Count;
                    foreach (var todo in team.CurrentTask.Todos)
                    {
                        if (todo.IsComplete)
                        {
                            done_counter++;
                        }
                    }
                    Console.WriteLine($"{done_counter / total * 100.0} %");
                }

                Console.ReadLine();
            }
        }
        private static void SeedTeamWithoutTask()
        {
            using (ProjektManagerContext context = new())
            {
                context.Add(new Team { Name = "NoTasksTeam"});
                context.SaveChanges();
            }
        }
        private static void PrintTeamCurrentTask()
        {
            using (ProjektManagerContext context = new())
            {
                var teams = context.Teams.Include(team => team.CurrentTask);
                foreach (var team in teams)
                {
                    string currentTask = "";
                    if (team.CurrentTask != null)
                        currentTask = team.CurrentTask.Name;
                    else
                        currentTask = "No task!";

                    Console.WriteLine($"Team: {team.Name} \t Task: {currentTask}");
                }
                Console.ReadLine();
            }
        }
        private static void PrintTeamsWithoutTasks()
        {
            using (ProjektManagerContext context = new())
            {
                var teams = context.Teams.Include(team => team.CurrentTask).Where(team => team.CurrentTask == null);
                foreach (var team in teams)
                {
                    Console.WriteLine($"This team needs to work!!!: {team.Name}");
                }
                Console.ReadLine();
            }
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

                task1.Todos.AddRange(new List<Todo> { todo1_1, todo1_2, todo1_3 });


                AddWorker(context, teamf, "Steen Secher", todo1_1);
                AddWorker(context, teamf, "Ejvind Møller", todo1_2);
                AddWorker(context, teamf, "Konrad Sommer", todo1_3);

                // Task 2
                Task task2 = new Task { Name = "Brew coffee"};
                Team teamb = new Team { Name = "Backend", CurrentTask = task2 };
                context.Add(task2);
                Todo todo2_1 = new Todo { Name = "Pour water", IsComplete = true };
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
                Todo todo3_1 = new Todo { Name = "Code code", IsComplete = true };
                Todo todo3_2 = new Todo { Name = "Compile the code", IsComplete = true };
                Todo todo3_3 = new Todo { Name = "Test ORM", IsComplete = true };

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

