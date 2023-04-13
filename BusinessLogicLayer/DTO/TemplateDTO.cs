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
    public class TemplateDTO
    {
        public Guid TemplateId { get; set; }
        public string? Name { get; set; }
        public string? Language { get; set; } 
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ProjectId { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }

        public ProjectDTO Project { get; set; }
        public  CreateEmployeeDTO TemplateCreatedBy { get; set; }
        public EmployeeDTO TemplateModifiedBy { get; set; }

    }
}
