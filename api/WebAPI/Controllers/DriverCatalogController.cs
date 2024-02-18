using Application;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi;

[ApiController]
[Route("api/drivercatalog")]
public class DriverCatalogController : ControllerBase
{
    private readonly ILogger<DriverCatalogController> _logger;
    private readonly DriverCatalogService _driverCatalogService;
    public DriverCatalogController(ILogger<DriverCatalogController> logger, DriverCatalogService driverCatalogService)
    {
        _logger = logger;
        _driverCatalogService = driverCatalogService;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var a = User!.Identity!.Name;
            return Ok(await _driverCatalogService.GetByIdAsync(id));
        }
        catch(Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpGet("paginated")]
    [Authorize]
    public async Task<IActionResult> GetPaginated(int skip, int take)
    {
        try
        {
            return Ok(await _driverCatalogService.GetPaginatedAsync(skip, take));
        }
        catch(Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateDriverCatalogDTO driverCatalog)
    {
        try
        {
            return CreatedAtAction(nameof(GetById), new { id = await _driverCatalogService.CreateAsync(driverCatalog) }, driverCatalog);
        }
        catch(Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody]UpdateDriverCatalogDTO driverCatalog)
    {
        try
        {
            await _driverCatalogService.UpdateAsync(id, driverCatalog, new Guid(User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value));
            return NoContent();
        }
        catch(Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _driverCatalogService.DeleteAsync(id, new Guid(User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value));
            return NoContent();
        }
        catch(Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }
}
