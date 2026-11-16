using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.CS360
{
    public class DeleteModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public DeleteModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Venue Venue { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venue = await _context.Venue.FirstOrDefaultAsync(m => m.venue_id == id);

            if (venue is not null)
            {
                Venue = venue;

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

            var venue = await _context.Venue.FindAsync(id);
            if (venue != null)
            {
                Venue = venue;
                _context.Venue.Remove(Venue);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
