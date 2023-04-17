namespace CloudPanelApi.App.Exceptions;

public class CloudPanelException : Exception
{
    public CloudPanelException()
    {
    }

    public CloudPanelException(string message) : base(message)
    {
    }

    public CloudPanelException(string message, Exception inner) : base(message, inner)
    {
    }
}