using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppLayeredDBDbContext _context;
        
        public ProjectRepository(AppLayeredDBDbContext context)
        {
            _context = context;
        }

        public List<Template> GetFilteredTemplatesByProject(Guid projectId )
        {
            var templates = new List<Template>();

            templates = _context.Templates
                .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project)
                .Where(x => x.ProjectId == projectId).Where(x => x.Name != "TEMPEmail").ToList();

            return templates;

        }
        public List<Template> GetFilteredTemplates(Guid? projectId = null, string language = null)
        {
            var templates = new List<Template>();

            if (projectId == null && language==null) {
                var alltemplates = new List<Template>();

                alltemplates = _context.Templates
                .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project).ToList();
                return alltemplates;
            }
            else if (projectId != null && language != null)
            {
                templates = _context.Templates
               .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project)
               .Where(x => x.Language == language && x.ProjectId == projectId).ToList();

                return templates;


            }
            else if (projectId != null )
            {
                templates = _context.Templates 
                .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project)
                .Where(x =>   x.ProjectId == projectId ).ToList();
     
                return templates;
            }
            else if (language != null)
            {
                templates = _context.Templates
                .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project)
                .Where(x => x.Language == language).ToList();

                return templates;
            }
           
            else
            {
                throw new Exception("Error!!!");
            }
            /*     templates = _context.Templates 
                 .Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).Include(x => x.Project)
                 .Where(x => ( projectId.HasValue? x.ProjectId == projectId : true ) && (language==null? x.Language == language : true)).ToList();
            */

        }

         public List<Project> GetAll()
         {
             List<Project> projects = new List<Project>();
             projects = _context.Projects.ToList();

             projects.ForEach(project =>
             {
                 project.CreatedDate = DateTime.ParseExact(project.CreatedDate.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

             });

             //projects = _context.Projects.Include(x => x.Templates).ToList();

             return projects;
         }

         public Project Add(Project projectRequest)
         {
             projectRequest.ProjectId = Guid.NewGuid();
             projectRequest.CreatedDate = DateTime.Now;
             _context.Projects.Add(projectRequest);
             _context.SaveChanges();
             return projectRequest;
         }

         public Project GetById(Guid id)
         {
             var project = _context.Projects.Include(x =>  x.Templates ).FirstOrDefault(x => x.ProjectId == id);
                 /*            _context.Templates.ToList();
 .ForEach( x => {
                 x.Templates.ToList().ForEach(x => { x.TemplateCreatedBy = this._employeeRepository.GetById((Guid)x.CreatedBy);
                     x.TemplateModifiedBy = this._employeeRepository.GetById((Guid)x.ModifiedBy);
                 });
             });*/
            if (project == null)
            {
                return null;
            }

            return project;
        }
        public void Delete(Guid id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                Console.WriteLine("cannot find this id !");
            }
            _context.Remove(project);
            _context.SaveChanges();
        }

        public Project GetByName(string name)
        {
            var project = _context.Projects.Include(x => x.Templates).FirstOrDefault(x => x.ProjectName == name);
            if (project == null)
            {
                return null;
            }

            return project;
        }
        public Project Update(Guid id, Project projectRequest)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return null;
            }
            if ((projectRequest.ProjectName != "") && (projectRequest.ProjectName != null))
            {
                project.ProjectName = projectRequest.ProjectName;
            }
            if ((projectRequest.CreatedBy != "") && (projectRequest.CreatedBy != null))
            {
                project.CreatedBy = projectRequest.CreatedBy;
            }
            _context.SaveChanges();

            return project;
        }
    }
}
