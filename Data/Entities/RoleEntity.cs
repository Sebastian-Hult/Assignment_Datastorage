using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class RoleEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string RoleName { get; set; } = null!;

    public ICollection<EmployeeEntity> Employees { get; set; } = [];
}
