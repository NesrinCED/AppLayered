using DataAccessLayer.DataContext;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppLayeredDBDbContext _context;

        public RoleRepository(AppLayeredDBDbContext context)
        {
            _context = context;
        }

        public Role Add(Role role)
        {
            role.RoleId = Guid.NewGuid();
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role;
        }

        public void Delete(Guid id)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
            {
                Console.WriteLine("cannot find this id !");
            }
            _context.Remove(role);

            _context.SaveChanges();
        }

        public List<Role> GetAll()
        {
            List<Role> roles = new List<Role>();

            roles = _context.Roles.Include(x => x.Employees).ToList();

            return roles;
        }

        public Role GetById(Guid id)
        {
            var role = _context.Roles.Include(x => x.Employees).FirstOrDefault(x => x.RoleId == id);
            
            if (role == null)
            {
                return null;
            }

            return role;
        }

        public Role GetByName(string name)
        {
            var role = _context.Roles.Include(x => x.Employees).FirstOrDefault(x => x.RoleName == name);

            if (role == null)
            {
                return null;
            }

            return role;
        }

        public Role Update(Guid id, Role roleRequest)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
            {
                return null;
            }
            if ((roleRequest.RoleName != "") && (roleRequest.RoleName != null))
            {
                role.RoleName = roleRequest.RoleName;
            }
            _context.SaveChanges();

            return role;
        }
    }
}
