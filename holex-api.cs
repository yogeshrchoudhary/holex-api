using holex_api.Models;
using holex_api.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace SideKick.Function;

public class holex_api(
    ILogger<holex_api> logger,
    IHolidayDataService dataService)
{
    [Function("holex_api_get")]
    public IEnumerable<HolidayItem> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "v1/holiday-items")] HttpRequestData req)
    {
        logger.LogInformation("GET called.");
        var exampleHolidayItems = dataService.GetHolidayItems();
        logger.LogInformation("Returning {Count} holiday items.", exampleHolidayItems?.Count() ?? 0);
        return exampleHolidayItems ?? [];
    }

    [Function("holex_api_post")]
    public void Post([HttpTrigger(AuthorizationLevel.Function, "post", Route = "v1/holiday-items")] HttpRequestData req,
        [FromBody] HolidayItem holidayItem)
    {
        logger.LogInformation("POST called.");
        dataService.AddHolidayItem(holidayItem);
        logger.LogInformation("Added holiday item with ID {Id}.", holidayItem.Id);
    }

    [Function("holex_api_delete")]
    public void Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "v1/holiday-items/{id:int}")] HttpRequestData req,
        int id)
    {
        logger.LogInformation("DELETE called for ID {Id}.", id);
        dataService.DeleteHolidayItem(id);
        logger.LogInformation("Deleted holiday item with ID {Id}.", id);
    }
}