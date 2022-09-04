using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Domain.Interface
{
    public interface IPostRepository
    {
        int AddNewPost(Post post);
        Post ShowPost(int id);
        void DeletePost(int id);
        IQueryable<Post> GetPosts();
        IQueryable<Post> GetUserPosts(string userId);
        IQueryable<Post> GetReportedPosts();
        void ReportPost(int postId);
        void CheckReportedPost(int postId);
        void BanPost(int postId);
        void UpdatePost(Post post);


        void AddNewParagraph(Paragraph paragraph);
        IQueryable<Paragraph> GetAttachedParagraphs(int postId);
        Paragraph GetParagraph(int paragraphId);
        int UpdateParagraph(Paragraph paragraph);
        void DeleteParagraph(int id);


        int AddNewGenre(Genre genre);
        Genre GetGenre(int id);
        IQueryable<Genre> GetGenres();
        int UpdateGenre(Genre genre);
        void DeleteGenre(int id);

        int AddComment(Comment comment);
        IQueryable<Comment> GetCommentsAttachedToPost(int postId);
        void DeleteComment(int id);

        ForumUser GetAuthor(string userId);
        bool CheckAuthor(int postId, string AuthorId);
        bool CheckParagraph(int postId, int paragraphId);
    }
}
