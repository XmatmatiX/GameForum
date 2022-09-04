using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Users
{
    public class UserForListVm : IMapFrom<ForumUser>
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ForumUser, UserForListVm>();
        }
    }
}
