using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace SideKick.Function;

public class holex_api
{
    private readonly ILogger<holex_api> _logger;

    public holex_api(ILogger<holex_api> logger)
    {
        _logger = logger;
    }

    [Function("holex_api")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/holiday-items")] HttpRequest req)
    {
        var exampleHolidayItems = new[]
        {
            new {
                id = 1,
                name = "Dorset",
                description = "Lulworth Cove, Dorset and the Jurassic Coast World Heritage Site",
                date = new DateTime(2025, 5, 22)
            },
            new {
                id = 2,
                name = "Cotswolds",
                description = "Lovely vilages of Bourton-on-the-Water, Cotswolds and Moreton-in-Marsh Market Town",
                date = new DateTime(2025, 3, 10)
            }
        };

        return new OkObjectResult(exampleHolidayItems);
    }
}