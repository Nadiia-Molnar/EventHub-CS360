using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using EventHub.Models;

namespace EventHub.Pages.Groups;

public class IndexModel : PageModel
{
    private readonly IMongoCollection<EventGroupDocument> _groups;

    public IndexModel(IMongoClient mongoClient)
    {
        _groups = mongoClient
            .GetDatabase("eventhub-db")
            .GetCollection<EventGroupDocument>("event_groups");
    }

    [BindProperty(SupportsGet = true)]
    public string Sort { get; set; } = "name";

    public List<EventGroupDocument> Groups { get; private set; } = [];

    public async Task OnGetAsync()
    {
        var sort = Sort == "capacity"
            ? Builders<EventGroupDocument>.Sort.Descending(group => group.MaxMembers)
            : Builders<EventGroupDocument>.Sort.Ascending(group => group.Name);

        Groups = await _groups
            .Find(_ => true)
            .Sort(sort)
            .ToListAsync();
    }
}