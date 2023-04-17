namespace CloudPanelApi.App.Http.Resources.Database;

public class MasterCredentials
{
    public string Host { get; set; } = "";
    public int Port { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}