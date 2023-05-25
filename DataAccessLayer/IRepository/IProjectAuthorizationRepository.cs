using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IProjectAuthorizationRepository
    {
        ProjectAuthorization GetById(Guid id);
        List<ProjectAuthorization> GetAll();
        ProjectAuthorization Add(ProjectAuthorization ProjectAuthorization);
        void Delete(Guid id);
        ProjectAuthorization Update(Guid id, ProjectAuthorization projAuthRequest);
        List<Employee> GetFilteredUsersByProject(Guid projectId);
        List<List<Template>> GetFilteredAccessedProjectAuth(Guid employeeID);
        List<Project> GetWriteAccessedProjects(Guid employeeID);
        List<Project> GetReadAccessedProjects(Guid employeeID);

    }
}
