﻿using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;

        private readonly IProjectService _projectService;

        public TemplateController(ITemplateService templateService,IProjectService projectService )
        {
            _templateService = templateService;
            _projectService = projectService;
        }

       [HttpPost]
        [Route("pdf/{id:Guid}")]
        public IActionResult CreatePDFWithTemplate([FromRoute] Guid id, [FromBody] Object json)
        {
            var fileData = _templateService.CreatePDFWithTemplate(id,json);
            var fileName = "TemplatePdf.pdf";
            var mimeType = "application/pdf";
            // Set the content type and return the PDF as a FileResult
            var file = File(fileData, mimeType, fileName);

            return file;
            //return Ok(file) ;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Please select an image.");

            return Ok(new { message = "Image uploaded successfully." });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_templateService.GetAll());
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateTemplateDTO addtemplateRequest)
        {
            if (addtemplateRequest == null)
            {
                return BadRequest("addtemplateRequest is empty");
            }
            return Ok(_templateService.Add(addtemplateRequest));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var template = _templateService.GetById(id);
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var template = _templateService.GetById(id);
            if (template == null)
            {
                return BadRequest("Employee Not Found !!!");
            }
            _templateService.Delete(id);

            return Ok(template);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, UpdateTemplateDTO updateTemplateRequest)
        {
            var template = _templateService.GetById(id);
            if (template == null)
            {
                return NotFound();
            }
            
            return Ok(_templateService.Update(id, updateTemplateRequest));
        }
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var template = _templateService.GetByName(name);
            if (template == null)
            {
                return NotFound();
            }
            return Ok(template);
        }


        [HttpPost]
        [Route("email/{id:Guid}")]
        public IActionResult SendEmailWithTemplate([FromRoute] Guid id, [FromBody] Object body)
        {
            try
            {
                var bodyS = JObject.Parse(body.ToString());

                string subject = bodyS["subject"].ToString();
                string to = bodyS["to"].ToString();
                var json = bodyS["jsonData"];

                return Ok(_templateService.SendEmailWithTemplate(id, subject, to, json));
                //return Ok("Email sent successfully !");
            }
            catch (SmtpException ex)
            {
                return BadRequest("Error sending email: " + ex.Message);
            }

        }

      
        [HttpPost]
        [Route("engine/{id:Guid}")]
        public IActionResult TemplateEngine([FromRoute] Guid id, [FromBody] Object json)
        {
            try
            {
                string outputContent=_templateService.GenerateTemplateEngine(id, json);
                return Ok(outputContent);
            }
            catch (SmtpException ex)
            {
                return BadRequest("Error generating template: " + ex.Message);
            }
        }
    }
}
