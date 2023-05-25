using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CreateProjectAuthorizationDTO
    {
        public Guid ProjectAuthorizationId { get; set; }
        public bool Read { get; set; } = false;
        public bool Write { get; set; } = false;
        public Guid? ProjectId { get; set; }
        public Guid? EmployeeId { get; set; }

    }
}
