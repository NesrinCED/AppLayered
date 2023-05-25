using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ITemplateHistoryRepository
    {
        Templatehistory GetById(Guid id);
        public List<Templatehistory> GetAll();
        List<Templatehistory> GetHistoricByTemplateId(Guid id);
        Templatehistory Add(Template templateRequest, string employeeName);
    }
}
