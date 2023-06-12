using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using iTextSharp.tool.xml.html;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NVelocity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _pRepository;
        private readonly IEmployeeRepository _eRepository;

        public TemplateService(ITemplateRepository templateRepository, IMapper mapper, IProjectRepository pRepository,IEmployeeRepository eRepository)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
            _eRepository=eRepository;
            _pRepository= pRepository;

        }
        public CreateTemplateDTO Add(CreateTemplateDTO addtemplateRequest)
        {           
            var mappedTemplate = _mapper.Map<DataAccessLayer.Models.Template>(addtemplateRequest);
            
            //i needed these cz it rerurns new id for employee and project !!

            mappedTemplate.TemplateCreatedBy = _eRepository.GetById((Guid)addtemplateRequest.CreatedBy);

            if(addtemplateRequest.ProjectId!=null)
            {
                mappedTemplate.Project = _pRepository.GetById((Guid)addtemplateRequest.ProjectId);
            }
            else
            {
                mappedTemplate.Project = null;
            }
            return _mapper.Map<CreateTemplateDTO>(_templateRepository.Add(mappedTemplate));
        }

        public void Delete(Guid id)
        {
            _templateRepository.Delete(id);
        }

        public List<TemplateDTO> GetAll()
        {
            var data = _templateRepository.GetAll();

            return _mapper.Map<List<TemplateDTO>>(data);

        }

        public TemplateDTO GetById(Guid id)
        {
            var data = _templateRepository.GetById(id);

            return _mapper.Map<TemplateDTO>(data);
        }

        public TemplateDTO GetByName(string name)
        {
            var data = _templateRepository.GetByName(name);

            return _mapper.Map<TemplateDTO>(data);
        }

        public UpdateTemplateDTO Update(Guid id, UpdateTemplateDTO updateTemplateRequest)
        {
            var mappedData = _mapper.Map<DataAccessLayer.Models.Template>(updateTemplateRequest);

            mappedData.TemplateModifiedBy = _eRepository.GetById((Guid)updateTemplateRequest.ModifiedBy);

            return _mapper.Map<UpdateTemplateDTO>(_templateRepository.Update(id, mappedData));
        }

        public byte[] CreatePDFWithTemplate(Guid id, Object json)
        {
            return _templateRepository.CreatePDFWithTemplate(id,json);
        }
        public Task<string> SendEmailWithTemplate(Guid templateId, string subject, string to, Object json)
        {
            return _templateRepository.SendEmailWithTemplate(templateId,subject,to,json);
        }
        public string GenerateTemplateEngine(Guid id, Object json)
        {
            return _templateRepository.GenerateTemplateEngine(id, json);
        }

        /*******Send email For User Password*********/
        public string SendPasswordEmailToUser(Guid idUser, string emailUser)
        {
            return _templateRepository.SendPasswordEmailToUser(idUser,  emailUser);
        }

    }
}
