// using Auth0.ManagementApi;
// using Auth0.ManagementApi.Models;
// using Domain.DTOs;
// using Infrastructure;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// namespace webapi;

// [ApiController]
// [Route("api/management")]
// public class Managementcontroller : ControllerBase
// {
//     private readonly ILogger<DriverCatalogController> _logger;
//     private readonly DriverCatalogService _driverCatalogService;
//     public Managementcontroller(ILogger<DriverCatalogController> logger, DriverCatalogService driverCatalogService)
//     {
//         _logger = logger;
//         _driverCatalogService = driverCatalogService;
//     }

//     [HttpGet]
//     [Authorize]
//     public async Task<IActionResult> GetUserInfo(string auth0Id)
//     {
//         try
//         {
//             var client = new ManagementApiClient("your token", new Uri("https://YOUR_AUTH0_DOMAIN/api/v2"));
//             var user = await client.Users.GetAsync(auth0Id);
//             user.UserId
//         }
//         catch(Exception exception)
//         {
//             _logger.LogError("{@exception}", exception);
//             return BadRequest();
//         }
//     }

//     [HttpPost]
//     [Authorize]
//     public async Task<IActionResult> CreateUser(CreateDriverCatalogDTO driverCatalog)
//     {
//         try
//         {
//             // var client = new ManagementApiClient("your token", new Uri("https://YOUR_AUTH0_DOMAIN/api/v2"));
//             // var user = await client.Users.CreateAsync(new UserCreateRequest
//             // {
//             //       Email = 
//             // });

//             // return CreatedAtAction(nameof(GetUserInfo), new { id = await _driverCatalogService.CreateDriverCatalogAsync(driverCatalog) }, driverCatalog);
//         }
//         catch(Exception exception)
//         {
//             _logger.LogError("{@exception}", exception);
//             return BadRequest();
//         }
//     }
// }
