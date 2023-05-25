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
    public class ProjectDTO
    {
        public Guid ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<TemplateDTO> FilteredTemplatesDTO { get; set; }
        public List<ProjectAuthorizationDTO> projectAuthorizationsDTO { get; set; }

    }
}
