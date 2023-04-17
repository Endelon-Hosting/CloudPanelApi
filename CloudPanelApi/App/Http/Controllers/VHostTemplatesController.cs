using CloudPanelApi.App.Exceptions;
using CloudPanelApi.App.Http.Resources.VHostTemplates;
using CloudPanelApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPanelApi.App.Http.Controllers;

[Route("vhosttemplates")]
[ApiController]
[Produces("application/json")]
public class VHostTemplatesController : Controller
{
    private readonly VHostTemplatesService VHostTemplatesService;

    public VHostTemplatesController(VHostTemplatesService vHostTemplatesService)
    {
        VHostTemplatesService = vHostTemplatesService;
    }

    [HttpGet]
    public async Task<ActionResult<VHostTemplate[]>> GetVHostTemplates()
    {
        try
        {
            var data = await VHostTemplatesService.GetTemplates();

            return Ok(data);
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

    [HttpPost]
    public async Task<ActionResult> ImportVHostTemplates()
    {
        try
        {
            await VHostTemplatesService.ImportTemplates();

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