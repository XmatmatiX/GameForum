using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Domain.Interface
{
    public interface IRoleRepository
    {
        IQueryable<IdentityRole> GetRoles();
        IdentityRole GetRole(string id);
        string AddRole(IdentityRole role);
        void DeleteRole(string id);
        void AttachRole(IdentityUserRole<string> userRole);
        void DetachRole(string userId, string roleId);
    }
}
