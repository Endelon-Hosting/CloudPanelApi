using System.ComponentModel.DataAnnotations;

namespace CloudPanelApi.App.Http.Requests.LetsEncrypt;

public class InstallCertificate
{
    [Required]
    public string DomainName { get; set; } = "";

    public List<string> SubjectAlternativeName { get; set; } = new();
}