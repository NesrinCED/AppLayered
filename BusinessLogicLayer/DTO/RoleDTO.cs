using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;

namespace BusinessLogicLayer.DTO
{
    public class RoleDTO
    {
        public Guid RoleId { get; set; }
        public string? RoleName { get; set; }
        public List<EmployeeDTO> EmployeesDTO { get; set; }
    }
}
