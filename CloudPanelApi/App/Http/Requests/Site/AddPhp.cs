using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.Site;

public class AddPhp
{
    [Required] public string DomainName { get; set; } = "";
    [Required] public string SiteUser { get; set; } = "";
    [Required] public string SiteUserPassword { get; set; } = "";
    [Required] public string VHostTemplate { get; set; } = "";
    [Required] public string PhpVersion { get; set; } = "";
}