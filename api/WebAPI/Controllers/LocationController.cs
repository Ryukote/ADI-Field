using Application.DTOs.Location;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly ILogger<LocationController> _logger;
    private readonly LocationService _locationService;
    public LocationController(ILogger<LocationController> logger, LocationService locationService)
    {
        _logger = logger;
        _locationService = locationService;
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var a = User!.Identity!.Name;
            return Ok(await _locationService.GetByIdAsync(id));
        }
        catch (Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpGet("paginated")]
    [Authorize]
    public async Task<IActionResult> GetPaginatedDriverCatalog(int skip, int take)
    {
        try
        {
            return Ok(await _locationService.GetPaginatedAsync(skip, take));
        }
        catch (Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create(CreateLocationDTO location)
    {
        try
        {
            return CreatedAtAction(nameof(GetById), new { id = await _locationService.CreateAsync(location) }, location);
        }
        catch (Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLocationDTO location)
    {
        try
        {
            await _locationService.UpdateAsync(id, location, new Guid(User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value));
            return NoContent();
        }
        catch (Exception exception)
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
            await _locationService.DeleteAsync(id, new Guid(User.Claims.FirstOrDefault(x => x.Type == "UserId")!.Value));
            return NoContent();
        }
        catch (Exception exception)
        {
            _logger.LogError("{@exception}", exception);
            return BadRequest();
        }
    }
}
