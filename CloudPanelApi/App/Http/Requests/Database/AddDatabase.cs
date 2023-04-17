using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.Database;

public class AddDatabase
{
    [Required] public string DomainName { get; set; } = "";
    [Required] public string DatabaseName { get; set; } = "";
    [Required] public string DatabaseUserName { get; set; } = "";
    [Required] public string DatabaseUserPassword { get; set; } = "";
}