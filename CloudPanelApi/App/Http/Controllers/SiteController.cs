using CloudPanelApi.App.Exceptions;
using CloudPanelApi.App.Http.Requests.Site;
using CloudPanelApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPanelApi.App.Http.Controllers;

[Route("site")]
[ApiController]
[Produces("application/json")]
public class SiteController : Controller
{
    private readonly SiteService SiteService;

    public SiteController(SiteService siteService)
    {
        SiteService = siteService;
    }

    [HttpPost("static")]
    public async Task<ActionResult> AddStatic([FromBody] AddStatic details)
    {
        try
        {
            await SiteService.AddStatic(
                details.DomainName,
                details.SiteUser,
                details.SiteUserPassword
            );

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
    
    [HttpPost("nodejs")]
    public async Task<ActionResult> AddNodeJs([FromBody] AddNodeJs details)
    {
        try
        {
            await SiteService.AddNodeJs(
                details.DomainName,
                details.SiteUser,
                details.SiteUserPassword,
                details.NodeJsVersion,
                details.AppPort
            );

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
    
    [HttpPost("python")]
    public async Task<ActionResult> AddPython([FromBody] AddPython details)
    {
        try
        {
            await SiteService.AddPython(
                details.DomainName,
                details.SiteUser,
                details.SiteUserPassword,
                details.PythonVersion,
                details.AppPort
            );

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
    
    [HttpPost("reverseproxy")]
    public async Task<ActionResult> AddReverseProxy([FromBody] AddReverseProxy details)
    {
        try
        {
            await SiteService.AddReverseProxy(
                details.DomainName,
                details.SiteUser,
                details.SiteUserPassword,
                details.ReverseProxyUrl
            );

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
    
    [HttpPost("php")]
    public async Task<ActionResult> AddPhp([FromBody] AddPhp details)
    {
        try
        {
            await SiteService.AddPhp(
                details.DomainName,
                details.SiteUser,
                details.SiteUserPassword,
                details.PhpVersion,
                details.VHostTemplate
            );

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

    [HttpDelete("{domainName}")]
    public async Task<ActionResult> Delete([FromRoute] string domainName)
    {
        try
        {
            await SiteService.Delete(
                domainName
            );

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