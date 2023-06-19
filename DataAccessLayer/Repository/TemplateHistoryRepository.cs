using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TemplateHistoryRepository : ITemplateHistoryRepository
    {
            
        private readonly AppLayeredDBDbContext _context;

        public TemplateHistoryRepository(AppLayeredDBDbContext context)
        {
            _context = context;
        }

        public List<Templatehistory> GetAll()
        {
            List<Templatehistory> templatehistorys = new List<Templatehistory>();

            templatehistorys = _context.Templatehistories.ToList(); //.Include(x => x.Template)

            return templatehistorys;
        }

        public Templatehistory GetById(Guid id)
        {
            var templatehistory = _context.Templatehistories.FirstOrDefault(x => x.TemplateHistoryId == id);//.Include(x => x.Template)

            if (templatehistory == null)
            {
                return null;
            }

            return templatehistory;
        }

        List<Templatehistory> ITemplateHistoryRepository.GetHistoricByTemplateId(Guid id)
        {
            var templatehistory = _context.Templatehistories.Where(a => a.TemplateId == id).ToList();

           /* templatehistory.ForEach(a =>
            {
                a.Content = $@"
                <html>
                <head>
                <style>
                table, th, td {{
                    border: 1px solid black;
                    border-collapse: collapse;
border-color: black; 
                }}
                </style>
                </head>
                <body>
                {a.Content}
                </body>
                </html>"; 
            });*/

            if (templatehistory == null)
            {
                return null;
            }

            return templatehistory;
        }
        public Templatehistory Add(Template templateRequest, string employeeName)
        {
            Templatehistory templatehistory = new Templatehistory() ;

            templatehistory.TemplateHistoryId = Guid.NewGuid();

            templatehistory.TemplateHistoryCreatedBy = employeeName;
            
            templatehistory.Content = templateRequest.Content;

            templatehistory.TemplateId = templateRequest.TemplateId;

            templatehistory.Template = templateRequest;

            _context.Templatehistories.Add(templatehistory);

            _context.SaveChanges();

            return templatehistory;
        }
        /* 
         public Templatehistory Update(Guid id, Templatehistory templatehistoryRequest)
         {
             var templatehistory = _context.Templatehistories.Find(id);

             if (templatehistory == null)
             {
                 return null;
             }
             if ((templatehistoryRequest.TemplateHistoryCreatedBy != "") && (templatehistoryRequest.TemplateHistoryCreatedBy != null))
             {
                 templatehistory.TemplateHistoryCreatedBy = templatehistoryRequest.TemplateHistoryCreatedBy;
             }
             _context.SaveChanges();

             return templatehistory;
         }


         public void Delete(Guid id)
         {
             var templatehistory = _context.Templatehistories.Find(id);

             if (templatehistory == null)
             {
                 Console.WriteLine("cannot find this id !");
             }
             _context.Remove(templatehistory);

             _context.SaveChanges();
         }*/
    }
}
