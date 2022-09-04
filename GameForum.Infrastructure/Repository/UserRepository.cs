using GameForum.Domain.Interface;
using GameForum.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;

        public UserRepository(Context context)
        {
            _context = context;
        }

        public void DeleteUser(string id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = false;
                _context.Attach(user);
                _context.Entry(user).Property("IsActive").IsModified = true;
                _context.SaveChanges();
            }
        }

        public IQueryable<ForumUser> GetRoleUsers(string roleId, bool isAttached)
        {
            var usersId = _context.UserRoles.Where(ur => ur.RoleId == roleId).Select(ur => ur.UserId);
            var users = _context.Users.Where(u => usersId.Contains(u.Id) == isAttached);
            return users;
        }

        public ForumUser GetUserDetails(string id)
        {
            var user = _context.Users.Where(u => u.Id == id).FirstOrDefault();
            var userPosts = _context.Posts.Where(up => up.isActive == true)
                .Where(up => up.ForumUserId == user.Id).ToList();
            foreach (var item in userPosts)
            {
                user.Posts.Add(item);
            }
            return user;
        }

        public IQueryable<Post> GetUserPosts(string userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            if (user.Posts == null)
            {
                return null;
            }
            var userPosts = user.Posts.Where(up => up.isActive == true).AsQueryable();
            return userPosts;
        }

        public IQueryable<ForumUser> GetUsers()
        {
            var users = _context.Users;
            return users.Where(u=>u.IsActive == true);
        }

        public string UpdateUser(ForumUser user)
        {
            var check = _context.Users.AsNoTracking().Where(u => u.Id == user.Id).FirstOrDefault();
            if (check == null)
            {
                return "";
            }
            _context.Attach(user);
            _context.Entry(user).Property("UserName").IsModified = true;
            _context.Entry(user).Property("Description").IsModified = true;
            _context.Entry(user).Property("PhoneNumber").IsModified = true;
            _context.SaveChanges();
            return user.Id;
        }
    }
}
