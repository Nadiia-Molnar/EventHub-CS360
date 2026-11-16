using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Events
{
    public class DeleteModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public DeleteModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EventCalendar EventCalendar { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventcalendar = await _context.EventCalendars.FirstOrDefaultAsync(m => m.event_id == id);

            if (eventcalendar is not null)
            {
                EventCalendar = eventcalendar;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventcalendar = await _context.EventCalendars.FindAsync(id);
            if (eventcalendar != null)
            {
                EventCalendar = eventcalendar;
                _context.EventCalendars.Remove(EventCalendar);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
