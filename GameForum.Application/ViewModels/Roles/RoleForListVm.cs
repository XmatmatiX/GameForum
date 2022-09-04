using AutoMapper;
using GameForum.Application.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Roles
{
    public class RoleForListVm : IMapFrom<IdentityRole>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<IdentityRole, RoleForListVm>();
        }
    }
}
