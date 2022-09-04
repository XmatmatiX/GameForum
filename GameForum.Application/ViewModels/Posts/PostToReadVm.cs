using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Application.ViewModels.Comments;
using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Application.ViewModels.Users;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Posts
{
    public class PostToReadVm : IMapFrom<Post>
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreateTime { get; set; }

        public GenreForListVm Genre { get; set; }

        public UserForListVm Author { get; set; }

        public List<ParagraphDetailsVm> Paragraphs { get; set; }

        public List<CommentDetailsVm> Comments { get; set; }

        public NewCommentVm NewComment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Post,PostToReadVm>()
                .ForMember(d=>d.Author,opt=>opt.MapFrom(s=>s.Author))
                .ForMember(d=>d.NewComment,opt=>opt.Ignore());
        }
    }
}
