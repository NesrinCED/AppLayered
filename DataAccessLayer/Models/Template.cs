using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Models;

[Table("template")]
public partial class Template
{
    [Key]
    [Column("Template_Id")]
    public Guid TemplateId { get; set; }
    [StringLength(100)]
    [Unicode(false)]
    public string? Name { get; set; }
    [StringLength(100)]
    [Unicode(false)]
    public string? Language { get; set; }

    [Unicode(false)]
    public string Content { get; set; } = null!;
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    [Column("Project_Id")]
    public Guid? ProjectId { get; set; }
    [Column("Created_By")]
    public Guid? CreatedBy { get; set; }
    [Column("Modified_By")]
    public Guid? ModifiedBy { get; set; }
    [ForeignKey("ProjectId")]
    [InverseProperty("Templates")]
    public virtual Project? Project { get; set; }
    [ForeignKey("CreatedBy")]
    [InverseProperty("TemplateCreatedBy")]
    public virtual Employee? TemplateCreatedBy { get; set; }
    [ForeignKey("ModifiedBy")]
    [InverseProperty("TemplateModifiedBy")]
    public virtual Employee? TemplateModifiedBy { get; set; }
}

