using CloudPanelApi.App.Exceptions;
using CloudPanelApi.App.Http.Requests.LetsEncrypt;
using CloudPanelApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPanelApi.App.Http.Controllers;

[Route("letsencrypt")]
[ApiController]
[Produces("application/json")]
public class LetsEncryptController : Controller
{
    private readonly LetsEncryptService LetsEncryptService;

    public LetsEncryptController(LetsEncryptService letsEncryptService)
    {
        LetsEncryptService = letsEncryptService;
    }

    [HttpPost("install/certificate")]
    public async Task<ActionResult> InstallCertificate([FromBody] InstallCertificate details)
    {
        try
        {
            await LetsEncryptService.InstallCertificate(details.DomainName, details.SubjectAlternativeName);

            return NoContent();
        }
        catch (CloudPanelException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}