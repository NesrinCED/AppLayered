using Azure.Core;
using BusinessLogicLayer.IServices;
using DataAccessLayer.DataContext;
using DataAccessLayer.Repository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.IRepository;
using AutoMapper;
using BusinessLogicLayer.DTO;
using static Azure.Core.HttpHeader;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMapper _mapper;
        public EmployeeService( IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
                _mapper = mapper;


        }
        public List<EmployeeDTO> GetAll()
        {
            var data = _employeeRepository.GetAll();
            var mappedData=_mapper.Map<List<EmployeeDTO>>(data);
            mappedData.ForEach(e=>
            {
                e.CreatedTemplatesDTO=_mapper.Map<List<CreateTemplateDTO>>(data.First(x=>x.EmployeeId==e.EmployeeId).TemplateCreatedBy);
                e.ModifiedTemplatesDTO = _mapper.Map<List<UpdateTemplateDTO>>(data.First(x => x.EmployeeId == e.EmployeeId).TemplateModifiedBy);
            });    
            
            return mappedData; 
        }
        public List<TemplateDTO> GetAllTemplates(Guid id)
        {
            var data = _employeeRepository.GetAllTemplates(id);
            var mappedData = _mapper.Map<List<TemplateDTO>>(data);
         /*   mappedData.ForEach(e =>
            {
                e.TemplateCreatedBy = _mapper.Map<List<CreateEmployeeDTO>>(data.First(x => x.EmployeeId == e.EmployeeId).);
                e.TemplateModifiedBy = _mapper.Map<List<UpdateTemplateDTO>>(data.First(x => x.EmployeeId == e.EmployeeId).TemplateModifiedBy);
            });*/

            return mappedData;

        }
        public EmployeeDTO GetById(Guid id)
        {
            var data = _employeeRepository.GetById(id);
            var mappedData = _mapper.Map<EmployeeDTO>(data);
            mappedData.CreatedTemplatesDTO = _mapper.Map<List<CreateTemplateDTO>>(data.TemplateCreatedBy);
            mappedData.ModifiedTemplatesDTO = _mapper.Map<List<UpdateTemplateDTO>>(data.TemplateModifiedBy);
            return mappedData;
        }
        public void Delete(Guid id)
        {
            _employeeRepository.Delete(id);
        }
        public  EmployeeDTO GetByName(string name)
        {
            var data = _employeeRepository.GetByName(name);
            var mappedData = _mapper.Map<EmployeeDTO>(data);
            mappedData.CreatedTemplatesDTO = _mapper.Map<List<CreateTemplateDTO>>(data.TemplateCreatedBy);
            mappedData.ModifiedTemplatesDTO = _mapper.Map<List<UpdateTemplateDTO>>(data.TemplateModifiedBy);
            return mappedData;
        }
        public  EmployeeDTO Update(Guid id, EmployeeDTO employeeRequest)
        {
            var mappedData = _mapper.Map<Employee>(employeeRequest);

            return _mapper.Map<EmployeeDTO>(_employeeRepository.Update(id,mappedData));
        }

        public CreateEmployeeDTO Register(CreateEmployeeDTO employeeRequest)
        {
            var mappedEmployee = _mapper.Map<Employee>(employeeRequest);
            return _mapper.Map<CreateEmployeeDTO>(_employeeRepository.Register(mappedEmployee));
        }

        public CreateEmployeeDTO Authenticate(CreateEmployeeDTO employeeRequest)
        {
            var mappedEmployee = _mapper.Map<Employee>(employeeRequest);
            return _mapper.Map<CreateEmployeeDTO>(_employeeRepository.Authenticate(mappedEmployee));
        }


    }
}
