using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

[Table("event_calendar", Schema = "dbo")]
public class EventCalendar
{
    [Key]
    public int event_id { get; set; }

    public int venue_id { get; set; }

    public int organizer_id { get; set; }

    [StringLength(100)]
    public string name { get; set; } = string.Empty;

    [StringLength(500)]
    public string description { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime date { get; set; }

    [DataType(DataType.Time)]
    public TimeOnly time { get; set; }

    [StringLength(100)]
    public string? category { get; set; }

    public DateTime created_at { get; set; }

    public DateTime updated_at { get; set; }
}