using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Posts
{
    public class PostForUserDetailsVm :IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostForUserDetailsVm>();
        }
    }
}
