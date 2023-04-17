using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.Site;

public class AddNodeJs
{
    [Required] public string DomainName { get; set; } = "";
    [Required] public string SiteUser { get; set; } = "";
    [Required] public string SiteUserPassword { get; set; } = "";
    [Required] public int NodeJsVersion { get; set; }
    [Required] public int AppPort { get; set; }
}