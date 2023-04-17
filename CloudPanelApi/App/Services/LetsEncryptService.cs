namespace CloudPanelApi.App.Services;

public class LetsEncryptService
{
    private readonly CliService CliService;

    public LetsEncryptService(CliService cliService)
    {
        CliService = cliService;
    }

    public async Task InstallCertificate(string domainName, List<string> subjectAlternativeNames)
    {
        if (subjectAlternativeNames.Any())
        {
            var domainList = "";

            foreach (var alternativeName in subjectAlternativeNames)
            {
                if (alternativeName == subjectAlternativeNames.Last())
                    domainList += alternativeName;
                else
                    domainList += $"{alternativeName},";
            }
            
            await CliService.Execute("lets-encrypt:install:certificate", args =>
            {
                args.Add("domainName", domainName);
                args.Add("subjectAlternativeName", domainList);
            });
        }
        else
        {
            await CliService.Execute("lets-encrypt:install:certificate", args =>
            {
                args.Add("domainName", domainName);
            });
        }
    }
}