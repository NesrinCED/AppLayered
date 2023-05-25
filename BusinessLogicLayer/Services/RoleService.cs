using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;

        }
        public CreateRoleDTO Add(CreateRoleDTO roleDTO)
        {
            var mappedRole = _mapper.Map<Role>(roleDTO);

            return _mapper.Map<CreateRoleDTO>(_roleRepository.Add(mappedRole));
        }

        public void Delete(Guid id)
        {
            _roleRepository.Delete(id);
        }

        public List<RoleDTO> GetAll()
        {
            var data = _roleRepository.GetAll();

            var mappedData = _mapper.Map<List<RoleDTO>>(data);

            mappedData.ForEach(e =>
            {
                e.EmployeesDTO = _mapper.Map<List<EmployeeDTO>>(data.First(x => x.RoleId == e.RoleId).Employees);
            });

            return mappedData;
        }

        public RoleDTO GetById(Guid id)
        {
            var data = _roleRepository.GetById(id);

            var mappedData = _mapper.Map<RoleDTO>(data);

            mappedData.EmployeesDTO = _mapper.Map<List<EmployeeDTO>>(data.Employees);

            return mappedData;
        }

        public RoleDTO GetByName(string name)
        {
            var data = _roleRepository.GetByName(name);

            var mappedData = _mapper.Map<RoleDTO>(data);

            mappedData.EmployeesDTO = _mapper.Map<List<EmployeeDTO>>(data.Employees);

            return mappedData;
        }

        public RoleDTO Update(Guid id, RoleDTO roleDTO)
        {
            var mappedData = _mapper.Map<Role>(roleDTO);

            return _mapper.Map<RoleDTO>(_roleRepository.Update(id, mappedData));
        }
    }
}