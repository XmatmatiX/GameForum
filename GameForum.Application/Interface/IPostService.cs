using GameForum.Application.ViewModels.Comments;
using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Application.ViewModels.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Interface
{
    public interface IPostService
    {
        ListPostForListVm GetPosts(int page, string searchString);
        ListUserPostForListVm GetUserPosts(string UserId, int page, string searchString);
        ListPostForListVm GetReportedPosts(int page);
        int AddNewPost(NewPostVm post);
        void AddNewGenre(string name);
        List<GenreForListVm> GetGenresList();
        GenreForListVm GetGenre(int genreId);
        PostToReadVm GetPost(int postId);
        ParagraphDetailsVm GetParagraph(int paragraphId);
        PostForUpdateVm GetPostForUpdate(int postId);
        int UpdatePost(PostForUpdateVm postVm);
        void UpdateGenre(GenreForListVm genre);
        int UpdateParagraph(ParagraphDetailsVm paragraph);
        void AddCommentToPost(NewCommentVm comment);
        void AttachParagraphToPost(NewParagraphVm paragraphVm);
        void ReportPost(int postId);
        void CheckPost(int postId);
        bool CheckAuthor(int postId, string authorId);
        bool CheckParagraph(int postId, int paragraphId);
        void BanPost(int postId);
        void DeleteParagraph(int paragraphId);
        void DeletePost(int postId);
        void DeleteGenre(int genreId);
    }
}
