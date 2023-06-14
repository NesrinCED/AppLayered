using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAuthorizationController : ControllerBase
    {
        private readonly IProjectAuthorizationService _ProjectAuthorizationService;

        public ProjectAuthorizationController(IProjectAuthorizationService ProjectAuthorizationService)
        {
            _ProjectAuthorizationService = ProjectAuthorizationService;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_ProjectAuthorizationService.GetAll());
        }
        [HttpGet("filteredEmployees/{projectId:Guid}")]
        public IActionResult GetFilteredUsersByProject(Guid projectId)
        {
            var listUsers = this._ProjectAuthorizationService.GetFilteredUsersByProject(projectId);

            return Ok(listUsers);

        }
        [HttpGet("filteredAccessedProjectAuth/{employeeId:Guid}")]
        public IActionResult GetFilteredAccessedProjectAuth(Guid employeeId)
        {
            var listWriteTemplates = this._ProjectAuthorizationService.GetFilteredAccessedProjectAuth(employeeId);

            return Ok(listWriteTemplates);

        }
        [HttpGet("GetReadTemplates/{employeeId:Guid}")]
        public IActionResult GetReadTemplates(Guid employeeId)
        {
            var listReadTemplates = this._ProjectAuthorizationService.GetReadTemplates(employeeId);

            return Ok(listReadTemplates);

        }
        
        /********** list projects write access ******/
        [HttpGet("WriteAccessedProjects/{employeeId:Guid}")]
        public IActionResult GetWriteAccessedProjects(Guid employeeId)
        {
            var listAccessedProjects = this._ProjectAuthorizationService.GetWriteAccessedProjects(employeeId);

            return Ok(listAccessedProjects);

        }     
        /********** list projects read access ******/
        [HttpGet("ReadAccessedProjects/{employeeId:Guid}")]
        public IActionResult GetReadAccessedProjects(Guid employeeId)
        {
            var listAccessedProjects = this._ProjectAuthorizationService.GetReadAccessedProjects(employeeId);

            return Ok(listAccessedProjects);

        }
        /********** isWriteProjByEmployee ******/
        [HttpGet("isWriteProjByEmployee/{projectId:Guid}/{employeeId:Guid}")]
        public IActionResult isWriteProjByEmployee(Guid projectId, Guid employeeId)
        {
            return Ok(this._ProjectAuthorizationService.isWriteProjByEmployee(projectId,employeeId));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var ProjectAuthorization = _ProjectAuthorizationService.GetById(id);
            if (ProjectAuthorization == null)
            {
                return NotFound();
            }
            return Ok(ProjectAuthorization);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var project = _ProjectAuthorizationService.GetById(id);
            if (project == null)
            {
                return BadRequest("project Not Found !!!");
            }
            _ProjectAuthorizationService.Delete(id);

            return Ok(project);
        }

    }
}
