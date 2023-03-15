using Discord;

namespace AverBot.Core.Activities;

public class WatchingActivity : IActivity
{
    public string Name { get; }
    public ActivityType Type { get; }
    public ActivityProperties Flags { get; }
    public string Details { get; }

    public WatchingActivity(string name)
    {
        Name = name;
        Type = ActivityType.Watching;
        Flags = ActivityProperties.None;
        Details = "";
    }
}