using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Events;

public class EditModel : PageModel
{
    private readonly EventHubContext _context;

    public EditModel(EventHubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public EventCalendar EventCalendar { get; set; } = default!;

    public SelectList VenueOptions { get; private set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var eventCalendar = await _context.EventCalendars
            .FirstOrDefaultAsync(item => item.event_id == id);

        if (eventCalendar is null)
        {
            return NotFound();
        }

        EventCalendar = eventCalendar;
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

        var existingEvent = await _context.EventCalendars
            .FirstOrDefaultAsync(item => item.event_id == EventCalendar.event_id);

        if (existingEvent is null)
        {
            return NotFound();
        }

        existingEvent.venue_id = EventCalendar.venue_id;
        existingEvent.organizer_id = EventCalendar.organizer_id;
        existingEvent.name = EventCalendar.name;
        existingEvent.description = EventCalendar.description;
        existingEvent.date = EventCalendar.date;
        existingEvent.time = EventCalendar.time;
        existingEvent.category = EventCalendar.category;
        existingEvent.updated_at = DateTime.Now;

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
            "name",
            EventCalendar?.venue_id);
    }
}