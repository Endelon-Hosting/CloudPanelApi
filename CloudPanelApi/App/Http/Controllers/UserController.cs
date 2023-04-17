using CloudPanelApi.App.Exceptions;
using CloudPanelApi.App.Http.Requests.User;
using CloudPanelApi.App.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudPanelApi.App.Http.Controllers;

[Route("user")]
[ApiController]
[Produces("application/json")]
public class UserController : Controller
{
    private readonly UserService UserService;

    public UserController(UserService userService)
    {
        UserService = userService;
    }

    [HttpPut("{username}/resetpassword")]
    public async Task<ActionResult> ResetPassword([FromRoute] string username, [FromBody] ResetPassword details)
    {
        try
        {
            await UserService.ResetPassword(username, details.Password);

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

    [HttpPut("{username}/mfa")]
    public async Task<ActionResult> DisableMfa([FromRoute] string username)
    {
        try
        {
            await UserService.DisableMfa(username);

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