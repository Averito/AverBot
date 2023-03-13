namespace PlatformBot.Core.Services;

public class EnvironmentService
{
    private string _path { get; }
    public string Path
    {
        get => _path;
    }

    public EnvironmentService(string path)
    {
        _path = path;
    }

    public void EnvironmentsLoad()
    {
        foreach (var environmentVar in File.ReadAllLines(_path))
        {
            var entries = environmentVar.Split("=");
            Environment.SetEnvironmentVariable(entries[0], entries[1]);   
        }
    }
}