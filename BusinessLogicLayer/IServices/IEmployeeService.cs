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
        CreateEmployeeDTO Register(CreateEmployeeDTO employeeRequest);
        CreateEmployeeDTO Authenticate(CreateEmployeeDTO employeeRequest);
        void Delete(Guid id);
        EmployeeDTO Update(Guid id, EmployeeDTO employeeRequest);
        public List<TemplateDTO> GetAllTemplates(Guid id);


    }
}
