using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.Site;

public class AddPython
{
    [Required] public string DomainName { get; set; } = "";
    [Required] public string SiteUser { get; set; } = "";
    [Required] public string SiteUserPassword { get; set; } = "";
    [Required] public string PythonVersion { get; set; }
    [Required] public int AppPort { get; set; }
}