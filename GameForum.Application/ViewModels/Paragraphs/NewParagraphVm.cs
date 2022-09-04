using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Paragraphs
{
    public class NewParagraphVm : IMapFrom<Paragraph>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool FirstParagraph { get; set; }

        public int PostId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewParagraphVm, Paragraph>();
        }
    }
}
