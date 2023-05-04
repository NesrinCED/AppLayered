using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppLayeredDBDbContext _context;
        public EmployeeRepository(AppLayeredDBDbContext context)
        {
            _context = context;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            employees = _context.Employees.ToList();

            employees = _context.Employees.Include(a=>a.TemplateCreatedBy).Include(a => a.TemplateModifiedBy).ToList();

            return employees;
        }

        public List<Template> GetAllTemplates(Guid id)
        {
            var employee=this.GetById(id);

            List<Template> templates = new List<Template>();

            templates = employee.TemplateCreatedBy.ToList();
            templates.Concat(employee.TemplateModifiedBy.ToList());

            return templates;

        }
        public Employee GetById(Guid id)
        {
            var employee =  _context.Employees.Include(x=>x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return null;
            }

            return employee;
        }
        public void Delete(Guid id)
        {
            var employee =  _context.Employees.Find(id);
            if (employee == null)
            { 
                Console.WriteLine("cannot find this id !"); 
            }
            _context.Remove(employee);
             _context.SaveChanges();
        }
        public Employee GetByName(string name)
        {
            var employee = _context.Employees.Include(x => x.TemplateCreatedBy).Include(x => x.TemplateModifiedBy).FirstOrDefault(x => x.EmployeeName == name);
            if (employee == null)
            {
                return null;
            }

            return employee;
        }

        public Employee Update(Guid id, Employee employeeRequest)
        {
            var employee =  _context.Employees.Find(id);

            if (employee == null)
            {
                return null;
            }
            if ((employeeRequest.EmployeeName != "") && (employeeRequest.EmployeeName != null))
            {
                employee.EmployeeName = employeeRequest.EmployeeName;
            }
            if ((employeeRequest.EmployeePassword != "") && (employeeRequest.EmployeePassword != null))
            {
                employee.EmployeePassword = employeeRequest.EmployeePassword;
            }
            _context.SaveChanges();

            return employee;
        }
        public Employee Register(Employee employeeRequest)
        {
            employeeRequest.EmployeeId = Guid.NewGuid();
            _context.Employees.Add(employeeRequest);
            _context.SaveChanges();
            return employeeRequest;
        }
        public Employee Authenticate(Employee employeeRequest)
         {
             var employee = _context.Employees
                         .FirstOrDefault(x => x.EmployeeName == employeeRequest.EmployeeName && x.EmployeePassword == employeeRequest.EmployeePassword);
             if (employee == null)
             {
                 return null;
             }
             return employee;
         }
 
    }
}
