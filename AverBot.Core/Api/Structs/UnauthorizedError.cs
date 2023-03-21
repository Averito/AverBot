namespace AverBot.Core.Structs;

public struct UnauthorizedError
{
    public int StatusCode { get; }
    public string Message { get; }

    public UnauthorizedError(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}