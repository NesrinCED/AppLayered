using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IProjectAuthorizationService
    {
        ProjectAuthorizationDTO GetById(Guid id);
        public List<ProjectAuthorizationDTO> GetAll();
        CreateProjectAuthorizationDTO Add(CreateProjectAuthorizationDTO ProjectAuthorization);
        void Delete(Guid id);
        ProjectAuthorizationDTO Update(Guid id, ProjectAuthorizationDTO projAuthRequest);
        List<EmployeeDTO> GetFilteredUsersByProject(Guid projectId);
        List<List<TemplateDTO>> GetFilteredAccessedProjectAuth(Guid employeeID);
        List<ProjectDTO> GetWriteAccessedProjects(Guid employeeID);
        List<ProjectDTO> GetReadAccessedProjects(Guid employeeID);


    }
}
