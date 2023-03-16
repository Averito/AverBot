namespace AverBot.API.Main.Services;

public class EnvironmentService
{
    private string Path { get; }

    public EnvironmentService(string path)
    {
        Path = path;
    }

    public void EnvironmentsLoad()
    {
        foreach (var environmentVar in File.ReadAllLines(Path))
        {
            var entries = environmentVar.Split("=");
            Environment.SetEnvironmentVariable(entries[0], entries[1]);   
        }
    }
}