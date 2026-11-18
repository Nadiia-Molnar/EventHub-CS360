using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Roles
{
    public class CreateModel : PageModel
    {
        private readonly EventHub.Data.EventHubContext _context;

        public CreateModel(EventHub.Data.EventHubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Role Role { get; set; } = default!;

        public SelectList ParentRoleOptions { get; private set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadParentRolesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Role.role_id");
            ModelState.Remove("Role.created_at");

            if (!ModelState.IsValid)
            {
                await LoadParentRolesAsync();
                return Page();
            }

            int maxId = await _context.Roles.MaxAsync(r => (int?)r.role_id) ?? 0;
            Role.role_id = maxId + 1;
            Role.created_at = DateTime.Now;

            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadParentRolesAsync()
        {
            var roles = await _context.Roles
                .AsNoTracking()
                .OrderBy(r => r.role_name)
                .ToListAsync();

            ParentRoleOptions = new SelectList(roles, "role_id", "role_name");
        }
    }
}
