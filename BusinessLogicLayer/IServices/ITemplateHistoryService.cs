using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ITemplateHistoryService
    {
        TemplateHistoryDTO GetById(Guid id);
        public List<TemplateHistoryDTO> GetAll();
        List<TemplateHistoryDTO> GetHistoricByTemplateId(Guid id);

    }
}
