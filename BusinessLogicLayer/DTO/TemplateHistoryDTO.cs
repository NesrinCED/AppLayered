using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class TemplateHistoryDTO
    {
        public Guid TemplateHistoryId { get; set; }
        public string? TemplateHistoryCreatedBy { get; set; }
        public string? Content { get; set; }
        public Guid? TemplateId { get; set; }
        public  TemplateDTO TemplateDTO { get; set; }
    }
}
