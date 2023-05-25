using BusinessLogicLayer.DTO;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IEmployeeService
    {
        EmployeeDTO GetById(Guid id);
        EmployeeDTO GetByName(string name);
        public List<EmployeeDTO> GetAll();
        CreateEmployeeDTO Add(CreateEmployeeDTO employeeRequest);
        AuthenticateEmployeeDTO Authenticate(AuthenticateEmployeeDTO employeeRequest);
        void Delete(Guid id);
        UpdateEmployeeDTO Update(Guid id, UpdateEmployeeDTO employeeRequest);
        UpdateUserByAdminDTO UpdateUserByAdmin(Guid id, UpdateUserByAdminDTO employeeRequest);

        public List<TemplateDTO> GetAllTemplates(Guid id);


    }
}
