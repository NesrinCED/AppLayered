using BusinessLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IRoleService
    {
        RoleDTO GetById(Guid id);
        RoleDTO GetByName(string name);
        public List<RoleDTO> GetAll();
        CreateRoleDTO Add(CreateRoleDTO roleDTO);
        void Delete(Guid id);
        RoleDTO Update(Guid id, RoleDTO roleDTO);
    }
}
