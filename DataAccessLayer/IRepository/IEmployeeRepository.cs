using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IEmployeeRepository
    {
        public List<Template> GetAllTemplates(Guid id);
        Employee GetById(Guid id);
        Employee GetByName(string name);
        public List<Employee> GetAll();
        Employee Register(Employee employeeRequest);
        Employee Authenticate(Employee employeeRequest);
        void Delete(Guid id);
        Employee Update(Guid id, Employee employeeRequest);

    }
}
