using Microsoft.AspNetCore.Mvc;
using BCA.Application.Interfaces;

namespace BCA.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // El controlador solo delega la responsabilidad
        var result = await _userService.GetUsersPagedAsync(page, pageSize);
        return Ok(result);
    }
}