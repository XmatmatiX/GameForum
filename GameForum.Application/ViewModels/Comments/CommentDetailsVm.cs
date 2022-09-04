using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Application.ViewModels.Users;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Comments
{
    public class CommentDetailsVm : IMapFrom<Comment>
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }

        public UserForListVm Author { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Comment, CommentDetailsVm>();
        }
    }
}
