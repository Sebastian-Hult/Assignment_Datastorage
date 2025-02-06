using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CurrencyEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string Currency { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}
