namespace yesenin.Qaraqulie.App;

public class TaskBundle
{
    private readonly List<TaskItem> _tasks = [];
    
    public TaskBundle AddTask(TaskItem task)
    {
        if (_tasks.Any(t => string.Equals(t.FileName, task.FileName, StringComparison.InvariantCultureIgnoreCase)))
        {
            throw new Exception($"Task with name '{task.FileName}' already exists");
        }
        _tasks.Add(task);
        return this;
    }

    public void Execute()
    {
        foreach (var task in _tasks)
        {
            task.Execute();
        }
    }
}