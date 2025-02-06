using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class UnitEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string Unit { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}
