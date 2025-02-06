using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ServiceEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(150)")]
    public string ServiceName { get; set; } = null!;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }


    public int UnitId { get; set; }
    public UnitEntity Unit { get; set; } = null!;

    public int CurrencyId { get; set; }
    public CurrencyEntity Currency { get; set; } = null!;


    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
