namespace AverBot.API.Constants;

public class ExceptionMessage
{
    public static string UserNotFound { get; } = "User not defined";
    public static string UserFound { get; } = "User was created";
    public static string UserIdNotValid { get; } = "Your userId not valid";
    public static string ServerFound { get; } = "Server with this id was created";
    public static string SomethingWentWrong { get; } = "Something went wrong... Try later.";
}