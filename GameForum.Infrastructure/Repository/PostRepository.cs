using GameForum.Domain.Interface;
using GameForum.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly Context _context;

        public PostRepository(Context context)
        {
            _context = context;
        }


        public int AddComment(Comment comment)
        {
            if (comment != null) 
            {
                comment.IsActive = true;
                comment.CreateTime = DateTime.Now;
                _context.Comments.Add(comment);
                _context.SaveChanges();
                return comment.Id;
            }
            return -1;
        }

        public int AddNewGenre(Genre genre)
        {
            if (genre != null) 
            {
                genre.IsActive = true;
                _context.Genres.Add(genre);
                _context.SaveChanges();
                return genre.Id;
            }
            return -1;
        }

        public void AddNewParagraph(Paragraph paragraph)
        {
            if (paragraph != null)
            {
                paragraph.IsActive = true;
                _context.Paragraphs.Add(paragraph);
                _context.SaveChanges();
            }
        }

        public int AddNewPost(Post post)
        {
            if (post != null)
            {
                post.isActive = true;
                post.IsChecked = false;
                post.IsReported = false;
                post.CreateTime = DateTime.Now;
                _context.Posts.Add(post);
                _context.SaveChanges();
                return post.Id;
            }
            return -1;
        }

        public void BanPost(int postId)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post != null)
            {
                post.IsBanned = true;
                post.IsChecked = true;
                _context.Attach(post);
                _context.Entry(post).Property(p => p.IsBanned).IsModified = true;
                _context.Entry(post).Property(p => p.IsChecked).IsModified = true;
                _context.SaveChanges();
            }
        }

        public bool CheckAuthor(int postId, string AuthorId)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                post = new Post();
            }
            if (post.ForumUserId == AuthorId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckParagraph(int postId, int paragraphId)
        {
            var paragraph = _context.Paragraphs.Where(p => p.Id == paragraphId).FirstOrDefault();
            if (paragraph == null)
            {
                paragraph = new Paragraph();
            }
            if (paragraph.PostId == postId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckReportedPost(int postId)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                return;
            }
            post.IsChecked = true;
            _context.Attach(post);
            _context.Entry(post).Property(p => p.IsChecked).IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.Where(c => c.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return;
            }
            comment.IsActive = false;
            _context.Attach(comment);
            _context.Entry(comment).Property("IsActive").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteGenre(int id)
        {
            var genre = _context.Genres.Where(g => g.Id == id).FirstOrDefault();
            if (genre == null)
            {
                return;
            }
            genre.IsActive = false;
            _context.Attach(genre);
            _context.Entry(genre).Property("IsActive").IsModified = true;
            _context.SaveChanges();
        }

        public void DeleteParagraph(int id)
        {
            var paragraph = _context.Paragraphs.Where(p => p.Id == id).FirstOrDefault();
            if (paragraph == null)
            {
                return;
            }
            paragraph.IsActive = false;
            _context.Attach(paragraph);
            _context.Entry(paragraph).Property("IsActive").IsModified = true;
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();
            if (post == null)
            {
                return;
            }
            post.isActive = false;
            _context.Attach(post);
            _context.Entry(post).Property("isActive").IsModified = true;
            _context.SaveChanges();
        }

        public IQueryable<Paragraph> GetAttachedParagraphs(int postId)
        {
            var paragraphs = _context.Paragraphs.Where(p => p.PostId == postId).Where(p=>p.IsActive);
            return paragraphs;
        }

        public ForumUser GetAuthor(string userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }

        public IQueryable<Comment> GetCommentsAttachedToPost(int postId)
        {
            var comments = _context.Comments.Where(c => c.PostId == postId).Where(c=>c.IsActive == true).AsQueryable();
            return comments;
        }

        public Genre GetGenre(int id)
        {
            var genre = _context.Genres.Where(g => g.Id == id).FirstOrDefault();
            return genre;
        }

        public IQueryable<Genre> GetGenres()
        {
            var genres = _context.Genres.Where(g => g.IsActive == true).AsQueryable();
            return genres;
        }

        public Paragraph GetParagraph(int paragraphId)
        {
            var paragraph = _context.Paragraphs.Where(p => p.Id == paragraphId).FirstOrDefault();
            return paragraph;
        }

        public IQueryable<Post> GetPosts()
        {
            var posts = _context.Posts.Where(p => p.isActive == true).Where(p=>p.IsBanned == false).AsQueryable();
            return posts;
        }

        public IQueryable<Post> GetReportedPosts()
        {
            var posts = _context.Posts.Where(p => p.IsReported).Where(p => p.IsChecked == false).AsQueryable();
            return posts;
        }

        public IQueryable<Post> GetUserPosts(string userId)
        {
            var posts = _context.Posts.Where(p => p.ForumUserId == userId).AsQueryable();
            return posts;
        }

        public void ReportPost(int postId)
        {
            var post = _context.Posts.Where(p => p.Id == postId).FirstOrDefault();
            if (post == null)
            {
                return;
            }
            post.IsReported = true;
            _context.Attach(post);
            _context.Entry(post).Property(p => p.IsReported).IsModified = true;
            _context.SaveChanges();
        }

        public Post ShowPost(int id)
        {
            var post = _context.Posts.Where(p => p.Id == id).FirstOrDefault();
            return post;
        }

        public int UpdateGenre(Genre genre)
        {
            var check = _context.Genres.AsNoTracking().Where(g => g.Id == genre.Id).FirstOrDefault();
            if (check == null)
            {
                return -1;
            }
            _context.Attach(genre);
            _context.Entry(genre).Property("Name").IsModified = true;
            _context.SaveChanges();
            return genre.Id;
        }

        public int UpdateParagraph(Paragraph paragraph)
        {
            var check = _context.Paragraphs.AsNoTracking().Where(p => p.Id == paragraph.Id).FirstOrDefault();
            if (check == null)
            {
                
                return -1;
            }
            _context.Attach(paragraph);
            _context.Entry(paragraph).Property("Title").IsModified = true;
            _context.Entry(paragraph).Property("Text").IsModified = true;
            var post = _context.Posts.AsNoTracking().Where(p => p.Id == paragraph.PostId).FirstOrDefault();
            if (post == null)
            {
                return -1;
            }
            _context.Attach(post);
            post.IsChecked = false;
            _context.Entry(post).Property("IsChecked").IsModified = true;
            _context.SaveChanges();
            return post.Id;
        }

        public void UpdatePost(Post post)
        {
            var check = _context.Posts.AsNoTracking().Where(p => p.Id == post.Id).FirstOrDefault();
            if (check == null)
            {
                return;
            }
            _context.SaveChanges();
            post.IsChecked = false;
            _context.Posts.Attach(post);
            _context.Entry(post).Property("Title").IsModified = true;
            _context.Entry(post).Property("Description").IsModified = true;
            _context.Entry(post).Property("IsChecked").IsModified = true;
            _context.SaveChanges();
        }

        private Genre getGenre(int genreId)
        {
            var genre = _context.Genres.Where(g => g.Id == genreId).FirstOrDefault();
            return genre;
        }

        private ForumUser getUser(string userId)
        {
            var user = _context.Users.Where(u => u.Id == userId).FirstOrDefault();
            return user;
        }


    }
}
