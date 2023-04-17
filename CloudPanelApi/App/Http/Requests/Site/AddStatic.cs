using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.Site;

public class AddStatic
{
    [Required] public string DomainName { get; set; } = "";
    [Required] public string SiteUser { get; set; } = "";
    [Required] public string SiteUserPassword { get; set; } = "";
}