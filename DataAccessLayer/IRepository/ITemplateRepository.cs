using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessLayer.IRepository
{
    public interface ITemplateRepository
    {
        Template GetById(Guid id);
        Template GetByName(string name);
        List<Template> GetAll();
        Template Add(Template templateRequest);
        void Delete(Guid id);
        Template Update(Guid id, Template templateRequest);
        byte[] CreatePDFWithTemplate(Guid id, Object json);
        string SendEmailWithTemplate(Guid templateId, string subject, string to, Object json);
        string GenerateTemplateEngine(Guid id, Object json);

    }
}



