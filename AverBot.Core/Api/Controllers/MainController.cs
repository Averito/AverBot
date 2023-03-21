using Microsoft.AspNetCore.Mvc;

namespace AverBot.Core.Controllers;

public class MainController<T> : ControllerBase
{
    protected readonly ILogger<T> _logger;

    public MainController(ILogger<T> logger)
    {
        _logger = logger;
    }
}