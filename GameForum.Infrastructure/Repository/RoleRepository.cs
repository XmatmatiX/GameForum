using GameForum.Domain.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Infrastructure.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Context _context;
        public RoleRepository(Context context)
        {
            _context = context;
        }

        public string AddRole(IdentityRole role)
        {
            if (role == null)
            {
                return "";
            }
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.Id;
        }

        public void AttachRole(IdentityUserRole<string> userRole)
        {
            if (userRole != null)
            {
                _context.UserRoles.Add(userRole);
                _context.SaveChanges();
            }
        }

        public void DeleteRole(string id)
        {
            var role = _context.Roles.Where(r => r.Id == id).FirstOrDefault();
            if (role!=null)
            {
                _context.Roles.Remove(role);
                _context.SaveChanges();
            }
        }

        public void DetachRole(string userId, string roleId)
        {
            var userRole = _context.UserRoles.Where(ur=>ur.RoleId == roleId)
                .Where(ur=>ur.UserId == userId).FirstOrDefault();
            if (userRole != null) 
            {
                _context.UserRoles.Remove(userRole);
                _context.SaveChanges();
            }
        }

        public IdentityRole GetRole(string id)
        {
            var role = _context.Roles.Where(r => r.Id == id).FirstOrDefault();
            return role;
        }

        public IQueryable<IdentityRole> GetRoles()
        {
            var roles = _context.Roles;
            return roles;
        }
    }
}
