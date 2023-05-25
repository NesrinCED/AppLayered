using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IRoleRepository
    {
        Role GetById(Guid id);
        Role GetByName(string name);
        public List<Role> GetAll();
        Role Add(Role role);
        void Delete(Guid id);
        Role Update(Guid id, Role role);
    }
}
