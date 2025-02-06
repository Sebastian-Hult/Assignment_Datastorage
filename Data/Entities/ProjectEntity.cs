using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(200)")]
    public string ProjectName { get; set; } = null!;

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }


    public int StatusId { get; set; }
    public StatusTypeEntity Status { get; set; } = null!;


    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;


    public int EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; } = null!;


    public int ServiceId { get; set; }
    public ServiceEntity Service { get; set; } = null!;
}
