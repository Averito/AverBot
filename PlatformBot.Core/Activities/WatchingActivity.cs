using Discord;

namespace PlatformBot.Core.Activities;

public class OnlineActivity : IActivity
{
    public string Name { get; }
    public ActivityType Type { get; }
    public ActivityProperties Flags { get; }
    public string Details { get; }

    public OnlineActivity(string name)
    {
        Name = name;
        Type = ActivityType.Watching;
        Flags = ActivityProperties.None;
        Details = "";
    }
}