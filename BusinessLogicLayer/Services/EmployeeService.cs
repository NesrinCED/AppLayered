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

        private readonly IProjectAuthorizationRepository _ProjectAuthorizationRepository;

        private readonly IMapper _mapper;
        public EmployeeService( IEmployeeRepository employeeRepository, IProjectAuthorizationRepository ProjectAuthorizationRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _ProjectAuthorizationRepository = ProjectAuthorizationRepository;

        }
        public List<EmployeeDTO> GetAll()
        {
            var data = _employeeRepository.GetAll();
            var mappedData=_mapper.Map<List<EmployeeDTO>>(data);
            mappedData.ForEach(e=>
            {
                e.RoleDTO= _mapper.Map<RoleDTO>(data.First(a=>a.Role==e.Role).RoleNavigation);
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
            mappedData.RoleDTO = _mapper.Map<RoleDTO>(data.RoleNavigation);
            mappedData.projectAuthorizationsDTO = _mapper.Map<List<CreateProjectAuthorizationDTO>>(data.ProjectAuthorizations);
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
            mappedData.RoleDTO = _mapper.Map<RoleDTO>(data.RoleNavigation);
            mappedData.CreatedTemplatesDTO = _mapper.Map<List<CreateTemplateDTO>>(data.TemplateCreatedBy);
            mappedData.ModifiedTemplatesDTO = _mapper.Map<List<UpdateTemplateDTO>>(data.TemplateModifiedBy);
            return mappedData;
        }
        public UpdateEmployeeDTO Update(Guid id, UpdateEmployeeDTO employeeRequest)
        {
            var mappedData = _mapper.Map<Employee>(employeeRequest);

            return _mapper.Map<UpdateEmployeeDTO>(_employeeRepository.Update(id,mappedData));
        }

        public CreateEmployeeDTO Add(CreateEmployeeDTO employeeRequest)
        {
            var mappedEmployee = _mapper.Map<Employee>(employeeRequest);

            var employee = _employeeRepository.Add(mappedEmployee);

            var createEmployeeDTO = _mapper.Map<CreateEmployeeDTO>(employee);

            createEmployeeDTO.projectAuthorizationsDTO = new List<CreateProjectAuthorizationDTO>();

            foreach (var dto in employeeRequest.projectAuthorizationsDTO)
            {
                dto.EmployeeId = createEmployeeDTO.EmployeeId;

                var mappedAuthorization = _mapper.Map<ProjectAuthorization>(dto);

                _ProjectAuthorizationRepository.Add(mappedAuthorization);

                createEmployeeDTO.projectAuthorizationsDTO.Add(dto);
            }
            return createEmployeeDTO;
        }

        public AuthenticateEmployeeDTO Authenticate(AuthenticateEmployeeDTO employeeRequest)
        {
            var mappedEmployee = _mapper.Map<Employee>(employeeRequest);
            return _mapper.Map<AuthenticateEmployeeDTO>(_employeeRepository.Authenticate(mappedEmployee));
        }

        UpdateUserByAdminDTO IEmployeeService.UpdateUserByAdmin(Guid id, UpdateUserByAdminDTO employeeRequest)
        {
            var mappedEmployee = _mapper.Map<Employee>(employeeRequest);

            var updatedEmployee = _employeeRepository.Update(id,mappedEmployee);

            var updatedEmployeeDTO = _mapper.Map<UpdateUserByAdminDTO>(updatedEmployee);

            updatedEmployeeDTO.projectAuthorizationsDTO = new List<CreateProjectAuthorizationDTO>();

            foreach (var dto in employeeRequest.projectAuthorizationsDTO)
            {
                dto.EmployeeId = id;

                var mappedAuthorization = _mapper.Map<ProjectAuthorization>(dto);

                _ProjectAuthorizationRepository.Update(mappedAuthorization.ProjectAuthorizationId,mappedAuthorization);

                updatedEmployeeDTO.projectAuthorizationsDTO.Add(dto);
            }
            return updatedEmployeeDTO;
            /* var mappedData = _mapper.Map<Employee>(employeeRequest);
             foreach (var dto in employeeRequest.projectAuthorizationsDTO)
             {
                 var authorization = _mapper.Map<ProjectAuthorization>(dto);
                 authorization.Employee = mappedData;
                 mappedData.ProjectAuthorizations.Add(authorization);
             }
             return _mapper.Map<UpdateUserByAdminDTO>(_employeeRepository.UpdateUserByAdmin(id, mappedData));*/
        }
    }
}
