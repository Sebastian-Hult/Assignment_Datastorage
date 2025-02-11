﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class CustomerRegistrationForm
{
    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Email { get; set; } = null!;
}
