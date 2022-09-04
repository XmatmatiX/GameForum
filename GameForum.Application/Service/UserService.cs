using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameForum.Application.Interface;
using GameForum.Application.ViewModels.Posts;
using GameForum.Application.ViewModels.Users;
using GameForum.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Service
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ListRoleUserForListVm GetRoleUsers(string roleId, int page, string searchString, bool isAttached)
        {
            var users = _userRepository.GetRoleUsers(roleId, isAttached)
                .ProjectTo<UserForListVm>(_mapper.ConfigurationProvider);
            var result = new ListRoleUserForListVm()
            {
                Users = users.Where(u => u.UserName.Contains(searchString)).Skip(20 * (page - 1)).Take(20).ToList(),
                Count = users.Count(),
                CurrentPage = page,
                SearchString = searchString,
                RoleId = roleId
            };
            return result;
        }

        public UserDetailsVm GetUserDetails(string id)
        {
            var userDetails = _userRepository.GetUserDetails(id);
            var userVm = _mapper.Map<UserDetailsVm>(userDetails);

            return userVm;
        }

        public ListUserForListVm GetUsers(int page, string searchString)
        {
            var users = _userRepository.GetUsers()
                .ProjectTo<UserForListVm>(_mapper.ConfigurationProvider);
            var result = new ListUserForListVm()
            {
                Users = users.Where(u => u.UserName.Contains(searchString)).Skip(20 * (page - 1)).Take(20).ToList(),
                Count = users.Count(),
                CurrentPage = page,
                SearchString = searchString
            };
            return result;
        }
    }
}
