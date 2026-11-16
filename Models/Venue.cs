using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

[Table("venue", Schema = "dbo")]
public class Venue
{
    [Key]
    public int venue_id { get; set; }

    public string name { get; set; } = string.Empty;

    public string address { get; set; } = string.Empty;

    public string city { get; set; } = string.Empty;

    public string state { get; set; } = string.Empty;

    public int capacity { get; set; }
}