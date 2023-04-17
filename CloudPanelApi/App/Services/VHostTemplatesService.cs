using CloudPanelApi.App.Http.Resources.VHostTemplates;

namespace CloudPanelApi.App.Services;

public class VHostTemplatesService
{
    private readonly CliService CliService;

    public VHostTemplatesService(CliService cliService)
    {
        CliService = cliService;
    }

    public async Task<VHostTemplate[]> GetTemplates()
    {
        var result = new List<VHostTemplate>();

        var data = await CliService.Execute("vhost-templates:list");
        
        // Split the input into lines and remove the first and last lines
        string[] lines = data.Split('\n');
        lines = lines[2..^2];

        // Parse each line into a dictionary
        List<Dictionary<string, string>> resultDic = new List<Dictionary<string, string>>();
        foreach (string line in lines)
        {
            string[] parts = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3) // Make sure all necessary parts exist
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                dictionary.Add("Name", parts[0].Trim());
                dictionary.Add("Root Directory", parts[1].Trim());
                dictionary.Add("Type", parts[2].Trim());
                resultDic.Add(dictionary);
            }
        }

        // Build the result
        foreach (Dictionary<string, string> dictionary in resultDic)
        {
            result.Add(new()
            {
                Name = dictionary["Name"],
                RootDirectory = dictionary.TryGetValue("Root Directory", out var value) ? value : "",
                Type = dictionary["Type"]
            });
        }
        
        return result.ToArray();
    }

    public async Task ImportTemplates()
    {
        await CliService.Execute("vhost-templates:import");
    }
}