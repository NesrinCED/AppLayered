using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Table("employee")]
public partial class Employee
{
    [Key]
    [Column("Employee_Id")]
    public Guid EmployeeId { get; set; }

    [Column("Employee_Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EmployeeName { get; set; }

    [Column("Employee_Password")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EmployeePassword { get; set; }

    [InverseProperty("TemplateCreatedBy")]
    public virtual ICollection<Template> TemplateCreatedBy { get; } = new List<Template>();

    [InverseProperty("TemplateModifiedBy")]
    public virtual ICollection<Template> TemplateModifiedBy { get; } = new List<Template>();
}
