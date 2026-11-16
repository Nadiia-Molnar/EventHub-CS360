using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Roles
{
    public class DetailsModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public DetailsModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

        public Role Role { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.role_id == id);

            if (role is not null)
            {
                Role = role;

                return Page();
            }

            return NotFound();
        }
    }
}
