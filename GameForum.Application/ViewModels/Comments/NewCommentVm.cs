using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Comments
{
    public class NewCommentVm : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string ForumUserId { get; set; }

        public int PostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewCommentVm, Comment>();
        }
    }
}
