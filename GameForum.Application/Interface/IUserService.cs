using GameForum.Application.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Interface
{
    public interface IUserService
    {
        UserDetailsVm GetUserDetails(string id);
        ListUserForListVm GetUsers(int page, string searchString);
        ListRoleUserForListVm GetRoleUsers(string roleId, int page, string searchString, bool isAttached);

    }
}
