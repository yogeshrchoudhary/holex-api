namespace holex_api.Models;

public class HolidayItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime Date { get; set; }
    public required string Description { get; set; }
}
