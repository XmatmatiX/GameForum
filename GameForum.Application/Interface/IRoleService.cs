using GameForum.Application.ViewModels.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Interface
{
    public interface IRoleService
    {
        ListRoleForListVm GetRoles();
        void AddNewRole(string name);
        void DeleteRole(string id);
        void AttachRole(string userId, string roleId);
        void DetachRole(string userId, string roleId);
        
    }
}
