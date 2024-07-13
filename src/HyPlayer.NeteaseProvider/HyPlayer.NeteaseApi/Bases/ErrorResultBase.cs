namespace HyPlayer.NeteaseApi.Bases;

public class ErrorResultBase : Exception
{
    public ErrorResultBase(int errorCode, string? errorMessage = null): base(errorMessage)
    {
        ErrorCode = errorCode;
    }

    public ErrorResultBase(Exception ex) : base(ex.Message)
    {
        ErrorCode = -500;
    }

    public int ErrorCode { get; set; }
}