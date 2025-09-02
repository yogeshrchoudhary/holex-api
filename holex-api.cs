using holex_api.Models;
using holex_api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace SideKick.Function;

public class holex_api(
    ILogger<holex_api> logger,
    IHolidayDataService dataService)
{
    [Function("holex_api")]
    public IActionResult Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/holiday-items")] HttpRequest req)
    {        
        logger.LogInformation("C# HTTP trigger function called.");
        var exampleHolidayItems = dataService.GetHolidayItems();
        logger.LogInformation("Returning {Count} holiday items.", exampleHolidayItems?.Count() ?? 0);
        return new OkObjectResult(exampleHolidayItems);
    }
}