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
    public class IndexModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public IndexModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

        public IList<EventCalendar> EventCalendar { get;set; } = default!;

        public async Task OnGetAsync()
        {
            EventCalendar = await _context.EventCalendars.ToListAsync();
        }
    }
}
