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
    public class DetailsModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public DetailsModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

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
    }
}
