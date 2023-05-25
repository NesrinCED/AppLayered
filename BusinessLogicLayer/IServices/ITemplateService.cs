using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ITemplateService
    {
        TemplateDTO GetById(Guid id);
        TemplateDTO GetByName(string name);
        public List<TemplateDTO> GetAll();
        CreateTemplateDTO Add(CreateTemplateDTO templateRequest);
        void Delete(Guid id);
        UpdateTemplateDTO Update(Guid id, UpdateTemplateDTO templateRequest);
        byte[] CreatePDFWithTemplate(Guid id, Object json);
        string SendEmailWithTemplate(Guid templateId, string subject, string to, Object json);
        string SendPasswordEmailToUser(Guid idUser, string emailUser);
        string GenerateTemplateEngine(Guid id, Object json);


    }
}