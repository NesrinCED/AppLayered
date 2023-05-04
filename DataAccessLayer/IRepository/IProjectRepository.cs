using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IProjectRepository
    {
        Project GetById(Guid id);
        Project GetByName(string name);
        public List<Project> GetAll();
        Project Add(Project projectRequest);
        void Delete(Guid id);
        Project Update(Guid id, Project projectRequest);
        public List<Template> GetFilteredTemplates(Guid projectId);

    }
}
