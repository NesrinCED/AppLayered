using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using iTextSharp.text.pdf;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Razor;
using NVelocity.App;
using NVelocity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using IronPdf;
using System.Text.RegularExpressions;
using System.Globalization;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Dynamic;
using Org.BouncyCastle.Crypto;

namespace DataAccessLayer.Repository
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly AppLayeredDBDbContext _context;

        private readonly IEmployeeRepository _employeeRepository;

        private readonly ITemplateHistoryRepository _templateHistoryRepository;
        public TemplateRepository(AppLayeredDBDbContext context, IEmployeeRepository employeeRepository, ITemplateHistoryRepository templateHistoryRepository)
        {
            _employeeRepository = employeeRepository;

            _templateHistoryRepository = templateHistoryRepository;

            _context = context;
        }

        public Models.Template Add(Models.Template templateRequest)
        {
            templateRequest.TemplateId = Guid.NewGuid();
            templateRequest.CreatedDate=DateTime.Now;
            _context.Templates.Add(templateRequest);

            /****insert into template history*****/
            var employeeName=this._employeeRepository.GetById((Guid)templateRequest.CreatedBy).EmployeeName;

            this._templateHistoryRepository.Add(templateRequest,employeeName);

            _context.SaveChanges();
            return templateRequest;
        }


        public void Delete(Guid id)
        {
            var template =  _context.Templates.Find(id);
            if (template == null)
            {
                Console.WriteLine("cannot find this id !");
            }
            _context.Remove(template);
            _context.SaveChanges();
        }
        public List<Models.Template> GetAll()
        {
            List<Models.Template> templates = new List<Models.Template>();

            templates = _context.Templates.Include(x=>x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x=>x.Project).ToList();
            //to get 2023-04-13T00.00.00
            templates.ForEach(template =>
            {
                template.CreatedDate = DateTime.ParseExact(template.CreatedDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                if (template.ModifiedDate!=null)
                {
                    template.ModifiedDate = DateTime.ParseExact(template.ModifiedDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }

            });

            return templates;
        }
      
        public Models.Template GetById(Guid id)
        {
            var template =  _context.Templates
                 .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project).FirstOrDefault(x => x.TemplateId == id);
            if (template == null)
            {
                return null;
            }

            return template;
        }

        public Models.Template Update(Guid id, Models.Template templateRequest)
        {
            var template =  _context.Templates.Find(id);

            if (template == null)
            {
                return null;
            }
            if ((templateRequest.Name != "") && (templateRequest.Name != null))
            {
                template.Name = templateRequest.Name;
            }
            if ((templateRequest.Language != "")&& (templateRequest.Language != null))
            {
                template.Language = templateRequest.Language;
            }
            if ((templateRequest.Content != "")&& (templateRequest.Content != null))
            {
                template.Content = templateRequest.Content;
            }
            if ((templateRequest.ProjectId != Guid.Empty)&& (templateRequest.ProjectId != null))
            {
                template.ProjectId = templateRequest.ProjectId;
            }
            if ((templateRequest.ModifiedBy != Guid.Empty) && (templateRequest.ModifiedBy != null))
            {
                template.ModifiedBy = templateRequest.ModifiedBy;
            }

            template.ModifiedDate= DateTime.Now;

            _context.SaveChanges();

            /****insert into template history*****/
            var employeeName = this._employeeRepository.GetById((Guid)templateRequest.ModifiedBy).EmployeeName;

            this._templateHistoryRepository.Add(template, employeeName);
            
            return template;
        }
        public Models.Template GetByName(string name)
        {
            var template = _context.Templates
                .Include(x => x.TemplateModifiedBy).Include(x => x.TemplateCreatedBy).Include(x => x.Project).FirstOrDefault(x => x.Name == name);
            if (template == null)
            {
                return null;
            }

            return template;
        }
      
        public byte[] CreatePDFWithTemplate(Guid id, Object json)
        {
            var template = GetById(id);

            string content = GenerateTemplateEngine(id, json);

            HtmlToPdf htmlToPdf = new HtmlToPdf();

            var pdfDocument = htmlToPdf.RenderHtmlAsPdf(content);

            pdfDocument.SaveAs("output.pdf");

            return pdfDocument.BinaryData.ToArray();

        }
        
        public async Task<string> SendEmailWithTemplate( Guid templateId, string subject, string to, Object json)
        {
            var template = GetById(templateId);
            
            string content = GenerateTemplateEngine(templateId,json);
            //var body = template.Content;
            var body = content;

            string fromMail = "dtgnororeply@gmail.com";
            var fromPassword = "acimfpkjvvyyazvi";
            // var fromPassword = "wrfyxjlylweyxano";

            MailMessage message = new MailMessage();

            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(to));
            message.IsBodyHtml = true;

            // Replace base64 image strings with image tags
            var regex = new Regex(@"<img\s+[^>]*?src\s*=\s*['""]data:(?<type>[^;]+);base64,(?<data>[^'""]+)['""][^>]*?>", RegexOptions.Compiled);
            content = regex.Replace(content, match =>
            {
                var type = match.Groups["type"].Value;
                var data = match.Groups["data"].Value;
                return $"<img src=\"data:{type};base64,{data}\" />";
            });
            
            message.Body = content;

            var smtpClient = new SmtpClient();

            smtpClient.UseDefaultCredentials = false;

            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            try
            {
                smtpClient.Send(message);
                return "Email sent successfully !";
            }
            catch (SmtpException ex)
            {
                return ex.Message;
            }

        }
        public string GenerateTemplateEngine(Guid id, Object json)
        {
            var engine = new VelocityEngine();

            engine.Init();

            var context = new VelocityContext();

            var template = GetById(id);

            var content = template.Content;

            dynamic model = JsonConvert.DeserializeObject(json.ToString());

            foreach (JProperty prop in model)
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var nestedObject = JsonConvert.DeserializeObject(prop.Value.ToString());
                    context.Put(prop.Name, nestedObject);
                }
                else
                {
                    context.Put(prop.Name, prop.Value.ToString());
                }
            }

            var outputWriter = new StringWriter();

            engine.Evaluate(context, outputWriter, "templateName", content);

            string output = outputWriter.ToString();

            return output;
        }
        /*******Send email For User Password*********/
        public string SendPasswordEmailToUser(Guid idUser, string emailUser)
        {
            var employee = _employeeRepository.GetById(idUser);

            var emailTemplateId = new Guid("0a692750-a67a-45b9-8108-5124499dc98e");

            var json = new JObject(
                new JProperty("name", employee.EmployeeName),
                new JProperty("password", employee.EmployeePassword)
            );

            string content = GenerateTemplateEngine(emailTemplateId, json);

            string fromMail = "dtgnororeply@gmail.com";
            var fromPassword = "acimfpkjvvyyazvi";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Password News";
            message.To.Add(new MailAddress(emailUser));
            message.IsBodyHtml = true;
            // Replace base64 image strings with image tags
            var regex = new Regex(@"<img\s+[^>]*?src\s*=\s*['""]data:(?<type>[^;]+);base64,(?<data>[^'""]+)['""][^>]*?>", RegexOptions.Compiled);
            content = regex.Replace(content, match =>
            {
                var type = match.Groups["type"].Value;
                var data = match.Groups["data"].Value;
                return $"<img src=\"data:{type};base64,{data}\" />";
            });
            message.Body = content;

            var smtpClient = new SmtpClient();

            smtpClient.UseDefaultCredentials = false;

            smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            try
            {
                smtpClient.Send(message);
                return "Email sent successfully !";
            }
            catch (SmtpException ex)
            {
                return ex.Message;
            }

        }
    }
}