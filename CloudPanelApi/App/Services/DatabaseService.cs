using CloudPanelApi.App.Http.Resources.Database;
using Logging.Net;

namespace CloudPanelApi.App.Services;

public class DatabaseService
{
    private readonly CliService CliService;

    public DatabaseService(CliService cliService)
    {
        CliService = cliService;
    }

    public async Task<MasterCredentials> ShowMasterCredentials()
    {
        var data = await CliService.Execute("db:show:master-credentials");

        // Split the output into lines and remove any leading/trailing white space
        string[] lines = data.Split('\n').Select(line => line.Trim()).ToArray();

        // Parse the values into a dictionary
        Dictionary<string, string> values = new Dictionary<string, string>();
        for (int i = 2; i < lines.Length - 1; i++)
        {
            string[] parts = lines[i].Split('|').Select(part => part.Trim()).ToArray();
            if (parts.Length >= 3)
            {
                values.Add(parts[1], parts[2]);
            }
        }

        return new()
        {
            Host = values["Host"],
            Port = int.Parse(values["Port"]),
            Username = values["User Name"],
            Password = values["Password"]
        };
    }

    public async Task Add(string domainName, string databaseName, string databaseUserName, string databaseUserPassword)
    {
        await CliService.Execute("db:add", args =>
        {
            args.Add("domainName", domainName);
            args.Add("databaseName", databaseName);
            args.Add("databaseUserName", databaseUserName);
            args.Add("databaseUserPassword", databaseUserPassword);
        });
    }

    public async Task Delete(string databaseName)
    {
        await CliService.Execute("db:delete", args =>
        {
            args.Add("databaseName", databaseName);
            args.Add("force", "");
        });
    }
}