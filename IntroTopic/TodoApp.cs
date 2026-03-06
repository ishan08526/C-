using System;
using System.Collections.Generic;

public static class TodoApp
{
    public static void Run()
    {
        List<string> todoList = new List<string>();
        Dictionary<string, List<int>> taskTags = new Dictionary<string, List<int>>(StringComparer.OrdinalIgnoreCase);

        Console.WriteLine("Welcome to the To-Do App");
        Console.WriteLine("You can use these commands: add [item], show, remove [index], clear, tag [index] [name], get-tagged [tag], exit");

        while (true)
        {
            Console.Write("> ");
            string? userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("Please type a command.");
                continue;
            }

            string[] inputParts = userInput.Trim().Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            string command = inputParts[0].ToLower();
            string extraInput = inputParts.Length > 1 ? inputParts[1] : "";

            switch (command)
            {
                case "add":
                    AddTask(todoList, extraInput);
                    break;

                case "show":
                    ShowTasks(todoList);
                    break;

                case "remove":
                    RemoveTask(todoList, taskTags, extraInput);
                    break;

                case "clear":
                    ClearTasks(todoList, taskTags);
                    break;

                case "tag":
                    AddTag(todoList, taskTags, extraInput);
                    break;

                case "get-tagged":
                    ShowTaggedTasks(todoList, taskTags, extraInput);
                    break;

                case "exit":
                    Console.WriteLine("Closing the app.");
                    return;

                default:
                    Console.WriteLine("That is not a valid command.");
                    break;
            }
        }
    }

    private static void AddTask(List<string> todoList, string extraInput)
    {
        if (string.IsNullOrWhiteSpace(extraInput))
        {
            Console.WriteLine("Please enter a task after typing add.");
            return;
        }

        todoList.Add(extraInput.Trim());
        Console.WriteLine("Task has been added.");
    }

    private static void ShowTasks(List<string> todoList)
    {
        if (todoList.Count == 0)
        {
            Console.WriteLine("Your to-do list is empty.");
            return;
        }

        for (int i = 0; i < todoList.Count; i++)
        {
            Console.WriteLine($"{i}: {todoList[i]}");
        }
    }

    private static void RemoveTask(List<string> todoList, Dictionary<string, List<int>> taskTags, string extraInput)
    {
        if (todoList.Count == 0)
        {
            Console.WriteLine("There are no tasks to remove.");
            return;
        }

        if (!int.TryParse(extraInput, out int index))
        {
            Console.WriteLine("Please enter a valid number for remove.");
            return;
        }

        if (index < 0 || index >= todoList.Count)
        {
            Console.WriteLine("That task number does not exist.");
            return;
        }

        todoList.RemoveAt(index);
        UpdateTagsAfterRemove(taskTags, index);
        Console.WriteLine("Task removed successfully.");
    }

    private static void ClearTasks(List<string> todoList, Dictionary<string, List<int>> taskTags)
    {
        todoList.Clear();
        taskTags.Clear();
        Console.WriteLine("All tasks have been cleared.");
    }

    private static void AddTag(List<string> todoList, Dictionary<string, List<int>> taskTags, string extraInput)
    {
        try
        {
            string[] tagParts = extraInput.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);

            if (tagParts.Length < 2)
            {
                throw new ArgumentException("Use: tag [index] [name]");
            }

            if (!int.TryParse(tagParts[0], out int index))
            {
                throw new ArgumentException("The index must be a number.");
            }

            if (index < 0 || index >= todoList.Count)
            {
                throw new IndexOutOfRangeException("That task index is out of range.");
            }

            string tagName = tagParts[1].Trim();

            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("Please enter a tag name.");
            }

            if (!taskTags.ContainsKey(tagName))
            {
                taskTags[tagName] = new List<int>();
            }

            if (taskTags[tagName].Contains(index))
            {
                throw new InvalidOperationException("That task already has this tag.");
            }

            taskTags[tagName].Add(index);
            Console.WriteLine($"Tag '{tagName}' added to task {index}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void ShowTaggedTasks(List<string> todoList, Dictionary<string, List<int>> taskTags, string extraInput)
    {
        try
        {
            string tagName = extraInput.Trim();

            if (string.IsNullOrWhiteSpace(tagName))
            {
                throw new ArgumentException("Use: get-tagged [tag]");
            }

            if (!taskTags.ContainsKey(tagName))
            {
                throw new KeyNotFoundException("That tag was not found.");
            }

            Console.WriteLine($"Tasks with tag '{tagName}':");

            foreach (int index in taskTags[tagName])
            {
                if (index >= 0 && index < todoList.Count)
                {
                    Console.WriteLine($"{index}: {todoList[index]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void UpdateTagsAfterRemove(Dictionary<string, List<int>> taskTags, int removedIndex)
    {
        foreach (var tag in taskTags)
        {
            tag.Value.RemoveAll(i => i == removedIndex);

            for (int i = 0; i < tag.Value.Count; i++)
            {
                if (tag.Value[i] > removedIndex)
                {
                    tag.Value[i]--;
                }
            }
        }
    }
}