using Microsoft.AspNetCore.Mvc;

namespace AverBot.API.Main.Controllers;

public class MainController<T> : ControllerBase
{
    public ILogger<T> Logger { get; }

    public MainController(ILogger<T> logger)
    {
        Logger = logger;
    }
}