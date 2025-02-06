using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class EmployeeEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;


    public int RoleId { get; set; }
    public RoleEntity Role { get; set; } = null!;


    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
