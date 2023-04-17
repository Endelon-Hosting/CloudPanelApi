using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.User;

public class ResetPassword
{
    [Required] public string Password { get; set; } = "";
}