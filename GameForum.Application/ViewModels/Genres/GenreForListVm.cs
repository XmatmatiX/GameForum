using AutoMapper;
using GameForum.Application.Mapping;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Genres
{
    public class GenreForListVm : IMapFrom<Genre>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Genre, GenreForListVm>().ReverseMap();
        }
    }
}
