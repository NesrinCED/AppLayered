using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.DTO
{
    public class EmployeeDTO
    {
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeePassword { get; set; }
        public Guid? Role { get; set; }
        public RoleDTO RoleDTO { get; set; }
        public string? EmployeeEmail { get; set; }
        public List<CreateProjectAuthorizationDTO> projectAuthorizationsDTO { get; set; }
        public List<CreateTemplateDTO> CreatedTemplatesDTO { get; set; }
        public List<UpdateTemplateDTO> ModifiedTemplatesDTO { get; set; }

    }
}
