using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Domain.Interface
{
    public interface IUserRepository
    {
        ForumUser GetUserDetails(string id);
        IQueryable<ForumUser> GetRoleUsers(string roleId, bool isAttached);
        string UpdateUser(ForumUser user);
        void DeleteUser(string id);
        IQueryable<ForumUser> GetUsers();
        IQueryable<Post> GetUserPosts(string userId);
    }
}
