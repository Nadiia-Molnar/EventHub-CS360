using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EventHub.Data;
using EventHub.Models;

namespace EventHub.Pages.Roles;

public class EditModel : PageModel
{
    private readonly EventHubContext _context;

    public EditModel(EventHubContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Role Role { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var role = await _context.Roles
            .FirstOrDefaultAsync(item => item.role_id == id);

        if (role is null)
        {
            return NotFound();
        }

        Role = role;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existingRole = await _context.Roles
            .FirstOrDefaultAsync(item => item.role_id == Role.role_id);

        if (existingRole is null)
        {
            return NotFound();
        }

        existingRole.parent_role_id = Role.parent_role_id;
        existingRole.role_name = Role.role_name;
        existingRole.description = Role.description;

        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}