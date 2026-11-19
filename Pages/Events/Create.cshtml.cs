
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Events;

public class CreateModel : PageModel
{
    private readonly EventHubContext _context;

    public CreateModel(EventHubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public EventCalendar EventCalendar { get; set; } = default!;

    public SelectList VenueOptions { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        await LoadVenueOptionsAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadVenueOptionsAsync();
            return Page();
        }

        EventCalendar.created_at = DateTime.Now;
        EventCalendar.updated_at = DateTime.Now;

        EventCalendar.event_id =
            (await _context.EventCalendars
                .MaxAsync(e => (int?)e.event_id) ?? 0) + 1;

        _context.EventCalendars.Add(EventCalendar);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }

    private async Task LoadVenueOptionsAsync()
    {
        var venues = await _context.Venue
            .AsNoTracking()
            .OrderBy(venue => venue.name)
            .ToListAsync();

        VenueOptions = new SelectList(
            venues,
            "venue_id",
            "name"
        );
    }
}
