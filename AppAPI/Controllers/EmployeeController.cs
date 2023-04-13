
using AutoMapper;
using BusinessLogicLayer.IServices;
using BusinessLogicLayer.DTO;
using DataAccessLayer.DataContext;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

using System.Net;
using System.Net.Mail;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly AppLayeredDBDbContext _context;
    
            public EmployeeController(IEmployeeService employeeService, AppLayeredDBDbContext context)
        {
            _employeeService = employeeService;
                _context = context;

            }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok( _employeeService.GetAll());
        }
        [HttpGet]
        [Route("AllTemplates/{id:Guid}")]
        public IActionResult GetAllTemplates([FromRoute] Guid id)
        {
            return Ok(_employeeService.GetAllTemplates(id));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
       [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var employee =  _employeeService.GetById(id);
            if (employee == null)
            {
                return BadRequest("Employee Not Found !!!");
            }
             _employeeService.Delete(id);

            return Ok(employee);
        }
        
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, EmployeeDTO employeeRequest)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_employeeService.Update(id,employeeRequest));

        }

       [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] CreateEmployeeDTO employeeRequest)
        {
            if(employeeRequest == null)
            {
                return BadRequest();
            }
            var employee = _employeeService.Authenticate(employeeRequest);

            if (employee == null)
            {
                return NotFound(new { Message = "UUser Not Found !"});
            }
            return Ok(
                employee
               // new { Message = "Login Success !" }
                );
        }
        
        [HttpPost("register")]
        public IActionResult Register([FromBody] CreateEmployeeDTO employeeRequest)
        {            
            if (employeeRequest == null)
            {
                return BadRequest(new { Message = "rahou null !" });
            }

            var employee = _employeeService.Register(employeeRequest);


            return Ok(
                employee
                // new { Message = "User Registered Success !" }
                );
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetByName([FromRoute] string name)
        {
            var employee = _employeeService.GetByName(name);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
      
       
    }

    
}
