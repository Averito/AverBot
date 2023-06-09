﻿namespace AverBot.Core.Infrastructure.Constants;

public class ExceptionMessage
{
    public static string NotFound { get; } = "Entity not found";
    public static string UserFound { get; } = "User was created";
    public static string UserIdNotValid { get; } = "Your userId not valid";
    public static string CurrentServerIdNotValid { get; } = "Your serverId not valid";
    public static string ServerFound { get; } = "Server with this id was created";
    public static string ServerNotFound { get; } = "Server with this id not found";
    public static string SomethingWentWrong { get; } = "Something went wrong... Try later.";
    public static string GuildUserFound { get; } = "GuildUser with this discordId was created";

}