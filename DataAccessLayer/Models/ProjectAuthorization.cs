using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    [Table("ProjectAuthorization")]
    public partial class ProjectAuthorization
    {
        [Key]
        [Column("ProjectAuthorization_Id")]
        public Guid ProjectAuthorizationId { get; set; }

        public bool Read { get; set; }

        public bool Write { get; set; }

        [Column("Project_Id")]
        public Guid? ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("ProjectAuthorizations")]
        public virtual Project? Project { get; set; }

        [Column("Employee_Id")]
        public Guid? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        [InverseProperty("ProjectAuthorizations")]
        public virtual Employee? Employee { get; set; }
    }

}
