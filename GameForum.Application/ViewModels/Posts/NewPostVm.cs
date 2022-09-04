using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Posts
{
    public class NewPostVm: IMapFrom<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string GenreId { get; set; }

        public string ForumUserId { get; set; }

        public List<NewParagraphVm> Paragraphs { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPostVm, Post>();
        }
    }
}
