using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IProjectService
    {
        ProjectDTO GetById(Guid id);
        ProjectDTO GetByName(string name);
        public List<ProjectDTO> GetAll();
        CreateProjectDTO Add(CreateProjectDTO projectRequest);
        void Delete(Guid id);
        ProjectDTO Update(Guid id, ProjectDTO projectRequest);
        public List<TemplateDTO> GetFilteredTemplatesByProject(Guid projectId);
        public List<TemplateDTO> GetFilteredTemplates(Guid? projectId = null, string language = null);

    }
}
