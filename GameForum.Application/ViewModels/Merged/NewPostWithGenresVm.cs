using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.MergedVm
{
    public class NewPostWithGenresVm
    {
        public NewPostVm Post { get; set; }
        public List<GenreForListVm> Genres { get; set; }
    }
}
