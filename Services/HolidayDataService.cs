using holex_api.Models;
using Microsoft.Extensions.Caching.Memory;

namespace holex_api.Services;

public interface IHolidayDataService
{
    void AddHolidayItem(HolidayItem holidayItem);
    IEnumerable<HolidayItem> GetHolidayItems();
}

internal class HolidayDataService(IMemoryCache memoryCache)
    : IHolidayDataService
{
    public IEnumerable<HolidayItem> GetHolidayItems()
    {
        var result = memoryCache.Get<IEnumerable<HolidayItem>>("HolidayItems");
        if (result == null)
        {
            var exampleItems = GetExampleHolidayItems().ToList();
            memoryCache.Set("HolidayItems", exampleItems);
            return exampleItems;
        }

        return memoryCache.Get<List<HolidayItem>>("HolidayItems")!;
    }
    
    private static IEnumerable<HolidayItem> GetExampleHolidayItems() => [
            new HolidayItem
            {
                Id = 1,
                Name = "Dorset",
                Description = "Lulworth Cove, Dorset and the Jurassic Coast World Heritage Site",
                Date = new DateTime(2025, 5, 22)
            },
            new HolidayItem
            {
                Id = 2,
                Name = "Cotswolds",
                Description = "Lovely villages of Bourton-on-the-Water, Cotswolds and Moreton-in-Marsh Market Town",
                Date = new DateTime(2025, 3, 10)
            }];

    public void AddHolidayItem(HolidayItem holidayItem)
    {
        var currentItems = GetHolidayItems().ToList();
        var newId = currentItems.Any() ? currentItems.Max(item => item.Id) + 1 : 1;
        holidayItem.Id = newId;
        currentItems.Add(holidayItem);
        memoryCache.Set("HolidayItems", currentItems);
    }
}