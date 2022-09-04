using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameForum.Application.Interface;
using GameForum.Application.ViewModels.Roles;
using GameForum.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Service
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

        public void AddNewRole(string name)
        {
            if (name is not null)
            {
                _roleRepository.AddRole(new IdentityRole(name));
            }
        }

        public void AttachRole(string userId, string roleId)
        {
            if (userId is not null && roleId is not null)
            {
                var userRole = new IdentityUserRole<string>() { RoleId = roleId, UserId = userId };
                _roleRepository.AttachRole(userRole);
            }
        }

        public void DeleteRole(string id)
        {
            if (id is not null)
            {
                _roleRepository.DeleteRole(id);
            }
        }

        public void DetachRole(string userId, string roleId)
        {
            if (userId is not null && roleId is not null)
            {
                _roleRepository.DetachRole(userId, roleId);
            }
        }

        public ListRoleForListVm GetRoles()
        {
            var result = new ListRoleForListVm();
            var roles = _roleRepository.GetRoles();
            if (roles == null) 
            {
                return result;
            }
            result.Roles = roles.ProjectTo<RoleForListVm>(_mapper.ConfigurationProvider).ToList();
            result.Count = roles.ToList().Count;
            return result;
        }
    }
}
