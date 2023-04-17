namespace CloudPanelApi.App.Services;

public class SiteService
{
    private readonly CliService CliService;

    public SiteService(CliService cliService)
    {
        CliService = cliService;
    }

    public async Task AddStatic(string domainName, string siteUser, string siteUserPassword)
    {
        await CliService.Execute("site:add:static", args =>
        {
            args.Add("domainName", domainName);
            args.Add("siteUser", siteUser);
            args.Add("siteUserPassword", siteUserPassword);
        });
    }

    public async Task AddNodeJs(string domainName, string siteUser, string siteUserPassword, int nodeJsVersion,
        int appPort)
    {
        await CliService.Execute("site:add:nodejs", args =>
        {
            args.Add("domainName", domainName);
            args.Add("siteUser", siteUser);
            args.Add("siteUserPassword", siteUserPassword);
            args.Add("nodejsVersion", nodeJsVersion.ToString());
            args.Add("appPort", appPort.ToString());
        });
    }

    public async Task AddPython(string domainName, string siteUser, string siteUserPassword, string pythonVersion,
        int appPort)
    {
        await CliService.Execute("site:add:python", args =>
        {
            args.Add("domainName", domainName);
            args.Add("siteUser", siteUser);
            args.Add("siteUserPassword", siteUserPassword);
            args.Add("pythonVersion", pythonVersion);
            args.Add("appPort", appPort.ToString());
        });
    }
    
    public async Task AddReverseProxy(string domainName, string siteUser, string siteUserPassword, string reverseProxyUrl)
    {
        await CliService.Execute("site:add:reverse-proxy", args =>
        {
            args.Add("domainName", domainName);
            args.Add("siteUser", siteUser);
            args.Add("siteUserPassword", siteUserPassword);
            args.Add("reverseProxyUrl", reverseProxyUrl);
        });
    }
    
    public async Task AddPhp(string domainName, string siteUser, string siteUserPassword, string phpVersion, string vhostTemplate)
    {
        await CliService.Execute("site:add:php", args =>
        {
            args.Add("domainName", domainName);
            args.Add("siteUser", siteUser);
            args.Add("siteUserPassword", siteUserPassword);
            args.Add("phpVersion", phpVersion);
            args.Add("vhostTemplate", vhostTemplate);
        });
    }

    public async Task Delete(string domainName)
    {
        await CliService.Execute("site:delete", args =>
        {
            args.Add("domainName", domainName);
            args.Add("force", "");
        });
    }
}