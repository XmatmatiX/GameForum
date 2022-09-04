using GameForum.Application.Interface;
using GameForum.Application.ViewModels.MergedVm;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Application.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Service
{
    public class MergedService : IMergedService
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        public MergedService(IPostService postService, IUserService userService)
        {
            _postService = postService;
            _userService = userService;
        }

        public void AddNewParagraph(NewParagraphVm newParagraph)
        {
            _postService.AttachParagraphToPost(newParagraph);
        }

        public int AddNewPost(NewPostWithGenresVm newPost)
        {
            return _postService.AddNewPost(newPost.Post);
        }

        public NewPostWithGenresVm GetNewPostTemplate()
        {
            var newTemplate = new NewPostWithGenresVm()
            {
                Post = new NewPostVm(),
                Genres = _postService.GetGenresList()
            };
            return newTemplate;
        }


    }
}
