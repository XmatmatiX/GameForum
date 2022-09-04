using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Users;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Posts
{
    public class PostForListVm : IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        public GenreForListVm Genre { get; set; }
        public UserForListVm Author { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostForListVm>();
        }
    }
}
