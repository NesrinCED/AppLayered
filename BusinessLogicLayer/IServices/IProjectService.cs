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
        ProjectDTO Add(ProjectDTO projectRequest);
        void Delete(Guid id);
        ProjectDTO Update(Guid id, ProjectDTO projectRequest);
    }
}
