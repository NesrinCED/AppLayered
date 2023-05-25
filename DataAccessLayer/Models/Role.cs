using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Table("role")]
public partial class Role
{
    [Key]
    [Column("Role_Id")]
    public Guid RoleId { get; set; }

    [Column("Role_Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? RoleName { get; set; }

   [InverseProperty("RoleNavigation")]
    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
