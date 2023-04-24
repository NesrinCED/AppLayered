using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DataAccessLayer.DataContext;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
           _projectService = projectService;

        }

        [HttpGet]
        [Route("filteredTemplates/{projectId:Guid}")]

        public IActionResult GetByProjectName([FromRoute] Guid projectId)
        {
            var project = this._projectService.GetById(projectId);
            var list = project.FilteredTemplatesDTO.ToList();

            return Ok(list);

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_projectService.GetAll());
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateProjectDTO projectRequest)
        {
            return Ok(_projectService.Add(projectRequest));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var project = _projectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var project = _projectService.GetById(id);
            if (project == null)
            {
                return BadRequest("project Not Found !!!");
            }
            _projectService.Delete(id);

            return Ok(project);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, ProjectDTO projectRequest)
        {
            var project = _projectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }
            
            return Ok(_projectService.Update(id, projectRequest));
        }
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var project = _projectService.GetByName(name);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

    }
}
