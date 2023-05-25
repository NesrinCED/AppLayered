using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ProjectAuthorizationRepository : IProjectAuthorizationRepository
    {
        private readonly AppLayeredDBDbContext _context;

        private readonly IEmployeeRepository _employeeRepository;
            
        public ProjectAuthorizationRepository(IEmployeeRepository employeeRepository,AppLayeredDBDbContext context)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }
        ProjectAuthorization IProjectAuthorizationRepository.Add(ProjectAuthorization ProjectAuthorization)
        {
            ProjectAuthorization.ProjectAuthorizationId = Guid.NewGuid();
            _context.ProjectAuthorizations.Add(ProjectAuthorization);
            _context.SaveChanges();
            return ProjectAuthorization;
        }

        void IProjectAuthorizationRepository.Delete(Guid id)
        {
            var ProjectAuthorization = _context.ProjectAuthorizations.Find(id);
            if (ProjectAuthorization == null)
            {
                Console.WriteLine("cannot find this id !");
            }
            _context.Remove(ProjectAuthorization);
            _context.SaveChanges();
        }

        List<ProjectAuthorization> IProjectAuthorizationRepository.GetAll()
        {
            List<ProjectAuthorization> ProjectAuthorizations = new List<ProjectAuthorization>();

            ProjectAuthorizations = _context.ProjectAuthorizations.Include(x => x.Employee).Include(x => x.Project).ToList();

            return ProjectAuthorizations;
        }

        ProjectAuthorization IProjectAuthorizationRepository.GetById(Guid id)
        {
            var ProjectAuthorization = _context.ProjectAuthorizations
                .Include(x => x.Employee).Include(x => x.Project).FirstOrDefault(x => x.ProjectAuthorizationId == id);
           
            if (ProjectAuthorization == null)
            {
                return null;
            }

            return ProjectAuthorization;
        }
        List<Employee> IProjectAuthorizationRepository.GetFilteredUsersByProject(Guid projectId)
        {
            var listProjAuth = _context.ProjectAuthorizations.Include( a => a.Employee).Include(a => a.Project)
                .Where( a => a.ProjectId == projectId ).Where(a => a.Read == true).ToList();

            var listEmployees = new List<Employee>();

            if (listProjAuth.Count()!=0)
            {
                listProjAuth.ForEach( a => {

                    var employee = _employeeRepository.GetById((Guid)a.EmployeeId);

                    listEmployees.Add(employee);
                } );
            }



            return listEmployees;
        }

        List<List<Template>> IProjectAuthorizationRepository.GetFilteredAccessedProjectAuth(Guid employeeID)
        {
            var listProjAuth = _context.ProjectAuthorizations.Include(a => a.Employee)
                .Include(a => a.Project).ThenInclude(p => p.Templates).ThenInclude(p => p.TemplateCreatedBy).ThenInclude(p => p.TemplateModifiedBy)
            .Where(a => a.EmployeeId == employeeID).Where(a => a.Write == true).ToList();

            var listProjects = new List<Project>();

            listProjects = listProjAuth.Select(a => a.Project).ToList();

            var listTemplates = new List<List<Template>>();

            listTemplates = listProjects.Select(a => a.Templates.ToList()).ToList();

            return listTemplates;
        }
        /**********list projects of write access****************/
        List<Project> IProjectAuthorizationRepository.GetWriteAccessedProjects(Guid employeeID)
        {
            var listProjAuth = _context.ProjectAuthorizations.Include(a => a.Employee)
           .Include(a => a.Project).ThenInclude(p => p.Templates).ThenInclude(p => p.TemplateCreatedBy).ThenInclude(p => p.TemplateModifiedBy)
           .Where(a => a.EmployeeId == employeeID).Where(a => a.Write == true).ToList();

            var listProjects = new List<Project>();

            listProjects = listProjAuth.Select(a => a.Project).ToList();

            return listProjects;
        }
        /**********list projects of read access****************/
        List<Project> IProjectAuthorizationRepository.GetReadAccessedProjects(Guid employeeID)
        {
            var listProjAuth = _context.ProjectAuthorizations.Include(a => a.Employee)
           .Include(a => a.Project).ThenInclude(p => p.Templates).ThenInclude(p => p.TemplateCreatedBy).ThenInclude(p => p.TemplateModifiedBy)
           .Where(a => a.EmployeeId == employeeID).Where(a => a.Read == true).ToList();

            var listProjects = new List<Project>();

            listProjects = listProjAuth.Select(a => a.Project).ToList();

            return listProjects;
        }

        ProjectAuthorization IProjectAuthorizationRepository.Update(Guid id, ProjectAuthorization projAuthRequest)
        {
            var projAuth = _context.ProjectAuthorizations.Find(id);

            if (projAuth == null)
            {
                return null;
            }
            if (projAuthRequest.Read != null)
            {
                projAuth.Read = projAuthRequest.Read;
            }
            if (projAuthRequest.Write != null)
            {
                projAuth.Write = projAuthRequest.Write;
            }
            _context.SaveChanges();

            return projAuth;
        }
    }
}
