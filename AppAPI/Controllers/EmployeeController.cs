
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
using BusinessLogicLayer.Services;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        private readonly ITemplateService _templateService;

        private readonly AppLayeredDBDbContext _context;
    
        public EmployeeController(IEmployeeService employeeService, ITemplateService templateService, AppLayeredDBDbContext context)
        {
            _employeeService = employeeService;
            _templateService = templateService;
            _context = context;
        }
        string GenerateRandomPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string password = new string(Enumerable.Repeat(chars, 8)
                                                  .Select(s => s[random.Next(s.Length)])
                                                  .ToArray());
            return password;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] CreateEmployeeDTO employeeRequest)
        {
            if (employeeRequest == null)
            {
                return BadRequest(new { Message = "employeeRequest is null !" });
            }

            employeeRequest.EmployeePassword = GenerateRandomPassword();

            var employee = _employeeService.Add(employeeRequest);

            var employeeId = employee.EmployeeId;

            var employeeEmail = employee.EmployeeEmail;

            if (employeeEmail != null)
            {
                _templateService.SendPasswordEmailToUser(employeeId, employeeEmail);
            }
            return Ok(employee);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_employeeService.GetAll());
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return BadRequest("Employee Not Found !!!");
            }
            _employeeService.Delete(id);

            return Ok(employee);
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
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, UpdateEmployeeDTO employeeRequest)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_employeeService.Update(id, employeeRequest));

        }

        [HttpPut]
        [Route("UpdateUserByAdmin/{id:Guid}")]
        public IActionResult UpdateUserByAdmin([FromRoute] Guid id, UpdateUserByAdminDTO employeeRequest)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(_employeeService.UpdateUserByAdmin(id, employeeRequest));

        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateEmployeeDTO employeeRequest)
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
