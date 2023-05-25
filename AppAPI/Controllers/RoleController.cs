using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_roleService.GetAll());
        }
        [HttpPost]
        public IActionResult Add([FromBody] CreateRoleDTO roleDTO)
        {
            return Ok(_roleService.Add(roleDTO));

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var role = _roleService.GetById(id);

            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var role = _roleService.GetById(id);

            if (role == null)
            {
                return BadRequest("role Not Found !!!");
            }
            _roleService.Delete(id);

            return Ok(role);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, RoleDTO roleDTO)
        {
            var role = _roleService.GetById(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(_roleService.Update(id, roleDTO));
        }
        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            var role = _roleService.GetByName(name);

            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

    }
    
}
