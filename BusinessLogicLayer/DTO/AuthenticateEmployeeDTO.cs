using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class AuthenticateEmployeeDTO
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeePassword { get; set; }
        public string? EmployeeEmail { get; set; }
        public Guid? Role { get; set; }
    }
}
