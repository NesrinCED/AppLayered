using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
     
        public ProjectService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository =  projectRepository;
            _mapper = mapper;

        }
        public ProjectDTO Add(ProjectDTO projectRequest)
        {
            var mappedProject = _mapper.Map<Project>(projectRequest);
            return _mapper.Map<ProjectDTO>(_projectRepository.Add(mappedProject));
        }
        public List<ProjectDTO> GetAll()
        {
            var data = _projectRepository.GetAll();
            var mappedData = _mapper.Map<List<ProjectDTO>>(data);
            mappedData.ForEach(e =>
            {
                e.TemplatesDTO = _mapper.Map<List<TemplateDTO>>(data.First(x => x.ProjectId == e.ProjectId).Templates);
            });

            return mappedData;
        }

        public void Delete(Guid id)
        {
            _projectRepository.Delete(id);
        }
        public ProjectDTO GetById(Guid id)
        {
            var data = _projectRepository.GetById(id);
            var mappedData = _mapper.Map<ProjectDTO>(data);
            mappedData.TemplatesDTO = _mapper.Map<List<TemplateDTO>>(data.Templates);
            return mappedData;
        }

        public ProjectDTO GetByName(string name)
        {
            var data = _projectRepository.GetByName(name);
            var mappedData = _mapper.Map<ProjectDTO>(data);
            mappedData.TemplatesDTO = _mapper.Map<List<TemplateDTO>>(data.Templates);
            return mappedData;
        }
        public ProjectDTO Update(Guid id, ProjectDTO projectRequest)
        {
            var mappedData = _mapper.Map<Project>(projectRequest);

            return _mapper.Map<ProjectDTO>(_projectRepository.Update(id, mappedData));
        }
    }
    }
