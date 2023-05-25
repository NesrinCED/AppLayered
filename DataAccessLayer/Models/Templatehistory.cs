using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("templatehistory")]
    public class Templatehistory
    {
        [Key]
        [Column("TemplateHistory_Id")]
        public Guid TemplateHistoryId { get; set; }

        [Column("TemplateHistory_CreatedBy")]
        [StringLength(100)]
        [Unicode(false)]
        public string? TemplateHistoryCreatedBy { get; set; }

        [Column("content")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Content { get; set; }

        [Column("Template_Id")]
        public Guid? TemplateId { get; set; }

        [ForeignKey("TemplateId")]
        [InverseProperty("Templatehistories")]
        public virtual Template? Template { get; set; }
    }
}
