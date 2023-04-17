namespace CloudPanelApi.App.Services;

public class UserService
{
    private readonly CliService CliService;

    public UserService(CliService cliService)
    {
        CliService = cliService;
    }

    public async Task ResetPassword(string username, string password)
    {
        await CliService.Execute("user:reset:password", args =>
        {
            args.Add("userName", username);
            args.Add("password", password);
        });
    }
    
    public async Task DisableMfa(string username)
    {
        await CliService.Execute("user:disable:mfa", args =>
        {
            args.Add("userName", username);
        });
    }
}