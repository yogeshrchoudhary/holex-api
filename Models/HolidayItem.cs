namespace holex_api.Models;
using Newtonsoft.Json;

public class HolidayItem
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("name")]
    public required string Name { get; set; }
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    [JsonProperty("description")]
    public required string Description { get; set; }
}
