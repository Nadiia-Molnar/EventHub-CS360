using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

[Table("roles", Schema = "dbo")]
public class Role
{
    [Key]
    public int role_id { get; set; }

    public int? parent_role_id { get; set; }

    [StringLength(100)]
    public string role_name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? description { get; set; }

    [Timestamp]
    public byte[] created_at { get; set; } = Array.Empty<byte>();
}