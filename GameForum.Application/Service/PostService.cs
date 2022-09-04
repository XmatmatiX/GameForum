using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameForum.Application.Interface;
using GameForum.Application.ViewModels.Comments;
using GameForum.Application.ViewModels.Genres;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Application.ViewModels.Posts;
using GameForum.Application.ViewModels.Users;
using GameForum.Domain.Interface;
using GameForum.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public void AddCommentToPost(NewCommentVm commentVm)
        {
            var comment = _mapper.Map<Comment>(commentVm);
            _postRepository.AddComment(comment);
        }

        public void AddNewGenre(string name)
        {
            if (name is not null)
            {
                var genre = new Genre() { IsActive = true, Name = name };
                _postRepository.AddNewGenre(genre);
            }
        }

        public int AddNewPost(NewPostVm postVm)
        {
            var post = _mapper.Map<Post>(postVm);
            var id = _postRepository.AddNewPost(post);
            return id;
        }

        public void AttachParagraphToPost(NewParagraphVm paragraphVm)
        {
            if (paragraphVm != null)
            {
                var paragraph = _mapper.Map<Paragraph>(paragraphVm);
                _postRepository.AddNewParagraph(paragraph);
            }
            
        }

        public void BanPost(int postId)
        {
            _postRepository.BanPost(postId);
        }

        public bool CheckAuthor(int postId, string authorId)
        {
            return _postRepository.CheckAuthor(postId, authorId);
        }

        public bool CheckParagraph(int postId, int paragraphId)
        {
            return _postRepository.CheckParagraph(postId, paragraphId);
        }

        public void CheckPost(int postId)
        {
            _postRepository.CheckReportedPost(postId);
        }

        public void DeleteGenre(int genreId)
        {
            _postRepository.DeleteGenre(genreId);
        }

        public void DeleteParagraph(int paragraphId)
        {
            _postRepository.DeleteParagraph(paragraphId);
        }

        public void DeletePost(int postId)
        {
            _postRepository.DeletePost(postId);
        }

        public GenreForListVm GetGenre(int genreId)
        {
            var genre = _postRepository.GetGenre(genreId);
            if (genre == null)
            {
                genre = new Genre();
            }
            var result = _mapper.Map<GenreForListVm>(genre);
            return result;
        }

        public List<GenreForListVm> GetGenresList()
        {
            var genres = _postRepository.GetGenres();
            if (genres == null)
            {
                return new List<GenreForListVm>();
            }
            var result = genres.ProjectTo<GenreForListVm>(_mapper.ConfigurationProvider).ToList();
            return result;
        }

        public ParagraphDetailsVm GetParagraph(int paragraphId)
        {
            var paragraph = _postRepository.GetParagraph(paragraphId);
            var result = _mapper.Map<ParagraphDetailsVm>(paragraph);
            return result;
        }

        public PostToReadVm GetPost(int postId)
        {
            var post = _postRepository.ShowPost(postId);
            var postVm = _mapper.Map<PostToReadVm>(post);
            var author = _postRepository.GetAuthor(post.ForumUserId);
            postVm.Author = _mapper.Map<UserForListVm>(author);
            postVm.Paragraphs = _postRepository.GetAttachedParagraphs(postId)
                .ProjectTo<ParagraphDetailsVm>(_mapper.ConfigurationProvider).ToList();
            postVm.Comments = _postRepository.GetCommentsAttachedToPost(post.Id)
                .ProjectTo<CommentDetailsVm>(_mapper.ConfigurationProvider).ToList();
            postVm.Comments.Reverse();
            var genre = _postRepository.GetGenre(post.GenreId);
            postVm.Genre = _mapper.Map<GenreForListVm>(genre);
            return postVm;
        }

        public PostForUpdateVm GetPostForUpdate(int postId)
        {
            var post = _postRepository.ShowPost(postId);
            var result = _mapper.Map<PostForUpdateVm>(post);
            result.Paragraphs = _postRepository.GetAttachedParagraphs(postId)
                .ProjectTo<ParagraphDetailsVm>(_mapper.ConfigurationProvider).ToList();
            return result;
        }

        public ListPostForListVm GetPosts(int page, string searchString)
        {
            var posts = _postRepository.GetPosts();
            if (posts ==null)
            {
                var emptyResult = new ListPostForListVm()
                {
                    Posts = new List<PostForListVm>(),
                    Count = 0,
                    CurrentPage = 1,
                    SearchString = ""
                };
                return emptyResult;
            }
            var postsVm = posts.ProjectTo<PostForListVm>(_mapper.ConfigurationProvider).ToList();
            postsVm.Reverse();
            var result = new ListPostForListVm()
            {
                Posts = postsVm.Where(p => p.Title.Contains(searchString)).Skip(20 * (page - 1)).Take(20).ToList(),
                Count = postsVm.Count,
                CurrentPage = page,
                SearchString = searchString
            };
            return result;
        }

        public ListPostForListVm GetReportedPosts(int page)
        {
            var posts = _postRepository.GetReportedPosts();
            if (posts == null)
            {
                var emptyResult = new ListPostForListVm()
                {
                    Posts = new List<PostForListVm>(),
                    Count = 0,
                    CurrentPage = 1
                };
                return emptyResult;
            }
            var postsVm = posts.ProjectTo<PostForListVm>(_mapper.ConfigurationProvider).Skip(20 * (page - 1)).Take(20).ToList();
            var result = new ListPostForListVm()
            {
                Posts = postsVm,
                Count = posts.Count(),
                CurrentPage = page
            };
            return result;
        }

        public ListUserPostForListVm GetUserPosts(string UserId, int page, string searchString)
        {
            var posts = _postRepository.GetUserPosts(UserId).Where(p => p.isActive).Where(p=>p.Title.Contains(searchString))
                .ProjectTo<UserPostForListVm>(_mapper.ConfigurationProvider).ToList();
            var list = new ListUserPostForListVm()
            {
                Count = 0,
                CurrentPage = 1,
                SearchString = searchString,
                UserId = UserId
            };
            if (posts == null)
            {
                list.PostList = new List<UserPostForListVm>();
                return list;
            }

            list.PostList = posts.Skip(20 * (page - 1)).Take(20).ToList();
            list.Count = posts.Count;

            return list;
        }

        public void ReportPost(int postId)
        {
            _postRepository.ReportPost(postId);
        }

        public void UpdateGenre(GenreForListVm genre)
        {
            var result = _mapper.Map<Genre>(genre);
            _postRepository.UpdateGenre(result);
        }

        public int UpdateParagraph(ParagraphDetailsVm paragraph)
        {
            var result = _mapper.Map<Paragraph>(paragraph);
            var postId = _postRepository.UpdateParagraph(result);

            return postId;
        }

        public int UpdatePost(PostForUpdateVm postVm)
        {
            var post = _mapper.Map<Post>(postVm);
            _postRepository.UpdatePost(post);
            return post.Id;
        }
    }
}
