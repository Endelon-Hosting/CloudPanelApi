using CloudPanelApi.App.Exceptions;
using CloudPanelApi.App.Http.Requests.Database;
using CloudPanelApi.App.Http.Resources.Database;
using CloudPanelApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPanelApi.App.Http.Controllers;

[Route("db")]
[ApiController]
[Produces("application/json")]
public class DatabaseController : Controller
{
    private readonly DatabaseService DatabaseService;

    public DatabaseController(DatabaseService databaseService)
    {
        DatabaseService = databaseService;
    }

    [HttpGet("master-credentials")]
    public async Task<ActionResult<MasterCredentials>> GetMasterCredentials()
    {
        try
        {
            var data = await DatabaseService.ShowMasterCredentials();
            
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
    public async Task<ActionResult> AddDatabase([FromBody] AddDatabase details)
    {
        try
        {
            await DatabaseService.Add(
                details.DomainName,
                details.DatabaseName,
                details.DatabaseUserName,
                details.DatabaseUserPassword
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

    [HttpDelete("{databaseName}")]
    public async Task<ActionResult> DeleteDatabase([FromRoute] string databaseName)
    {
        try
        {
            await DatabaseService.Delete(
                databaseName
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