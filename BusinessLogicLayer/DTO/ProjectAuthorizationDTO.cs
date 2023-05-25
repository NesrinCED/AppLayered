using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class ProjectAuthorizationDTO
    {
        public Guid ProjectAuthorizationId { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? EmployeeId { get; set; }
        public CreateEmployeeDTO EmployeeDTO { get; set; }
        public ProjectDTO ProjectDTO { get; set; }

    }
}
