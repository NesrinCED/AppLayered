using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProjectAuthorizationService : IProjectAuthorizationService
    {
        private readonly IProjectAuthorizationRepository _ProjectAuthorizationRepository;
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ProjectAuthorizationService(IProjectAuthorizationRepository ProjectAuthorizationRepository, IMapper mapper, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            _ProjectAuthorizationRepository = ProjectAuthorizationRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;

        }

        void IProjectAuthorizationService.Delete(Guid id)
        {
            _ProjectAuthorizationRepository.Delete(id);
        }

        List<ProjectAuthorizationDTO> IProjectAuthorizationService.GetAll()
        {
            var data = _ProjectAuthorizationRepository.GetAll();

            return _mapper.Map<List<ProjectAuthorizationDTO>>(data);
        }

        ProjectAuthorizationDTO IProjectAuthorizationService.GetById(Guid id)
        {
            var data = _ProjectAuthorizationRepository.GetById(id);

            return _mapper.Map<ProjectAuthorizationDTO>(data);
        }
        List<EmployeeDTO> IProjectAuthorizationService.GetFilteredUsersByProject(Guid projectId)
        {
            return _mapper.Map<List<EmployeeDTO>>(_ProjectAuthorizationRepository.GetFilteredUsersByProject(projectId));
        }
        List<List<TemplateDTO>> IProjectAuthorizationService.GetFilteredAccessedProjectAuth(Guid employeeID)
        {
            return _mapper.Map<List<List<TemplateDTO>>>(_ProjectAuthorizationRepository.GetFilteredAccessedProjectAuth(employeeID));
        }
        List<ProjectDTO> IProjectAuthorizationService.GetWriteAccessedProjects(Guid employeeID)
        {
            return _mapper.Map<List<ProjectDTO>>(_ProjectAuthorizationRepository.GetWriteAccessedProjects(employeeID));
        }
        List<ProjectDTO> IProjectAuthorizationService.GetReadAccessedProjects(Guid employeeID)
        {
            return _mapper.Map<List<ProjectDTO>>(_ProjectAuthorizationRepository.GetReadAccessedProjects(employeeID));
        }

        CreateProjectAuthorizationDTO IProjectAuthorizationService.Add(CreateProjectAuthorizationDTO ProjectAuthorization)
        {
            /*var mappedProjectAuthorization = _mapper.Map<DataAccessLayer.Models.ProjectAuthorization>(ProjectAuthorization);

            //i needed these cz it rerurns new id for employee and project !!

            mappedProjectAuthorization.Project = _projectRepository.GetById((Guid)ProjectAuthorization.ProjectDTO);

            if (addProjectAuthorizationRequest.ProjectId != null)
            {
                mappedProjectAuthorization.Project = _pRepository.GetById((Guid)ProjectAuthorization.ProjectId);
            }
            else
            {
                mappedProjectAuthorization.Project = null;
            }
            return _mapper.Map<CreateProjectAuthorizationDTO>(_ProjectAuthorizationRepository.Add(mappedProjectAuthorization));*/
            return null;
        }

        ProjectAuthorizationDTO IProjectAuthorizationService.Update(Guid id, ProjectAuthorizationDTO projAuthRequest)
        {
            var mappedData = _mapper.Map<ProjectAuthorization>(projAuthRequest);

            return _mapper.Map<ProjectAuthorizationDTO>(_ProjectAuthorizationRepository.Update(id, mappedData));
        }
    }
}
