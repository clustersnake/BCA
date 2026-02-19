using Microsoft.AspNetCore.Mvc;
using BCA.Domain.Interfaces;

namespace BCA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _repository;

    public UsersController(IUserRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // Validamos para que no nos pidan cosas locas como pageSize=0
        if (pageSize <= 0) pageSize = 10;
        if (page <= 0) page = 1;

        var result = await _repository.GetPagedAsync(page, pageSize);
        return Ok(result);
    }
}