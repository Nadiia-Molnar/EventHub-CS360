using Microsoft.EntityFrameworkCore;
using EventHub.Models;

namespace EventHub.Data;

public class EventHubContext : DbContext
{
    public EventHubContext(DbContextOptions<EventHubContext> options)
        : base(options)
    {
    }

    public DbSet<Venue> Venue { get; set; } = default!;

    public DbSet<EventCalendar> EventCalendars { get; set; } = default!;

    public DbSet<Role> Roles { get; set; } = default!;
}