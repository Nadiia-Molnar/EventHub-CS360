using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventHub.Models;

[Table("roles", Schema = "dbo")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int role_id { get; set; }

    public int? parent_role_id { get; set; }

    [StringLength(100)]
    public string role_name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? description { get; set; }

    public DateTime created_at { get; set; }
}
