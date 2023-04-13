using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Table("project")]
public partial class Project
{
    [Key]
    [Column("Project_Id")]
    public Guid ProjectId { get; set; }

    [Column("Project_Name")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ProjectName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<Template> Templates { get; } = new List<Template>();
}
