using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public class CreateTemplateDTO
    {
        public Guid TemplateId { get; set; }
        public string? Name { get; set; }
        public string? Language { get; set; }
        public string? Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? ProjectId { get; set; }

        public Guid? CreatedBy { get; set; }
    }
}
