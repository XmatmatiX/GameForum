using GameForum.Application.Interface;
using GameForum.Application.ViewModels.Comments;
using GameForum.Application.ViewModels.Paragraphs;
using GameForum.Application.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameForum.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostService _postService;
        public BlogController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var posts = _postService.GetPosts(1,"");

            return View(posts);
        }

        [HttpPost]
        public IActionResult Index(int? page, string searchString)
        {
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            if (!page.HasValue)
            {
                page = 1;
            }

            var posts = _postService.GetPosts(page.Value, searchString);

            return View(posts);
        }

        [HttpGet]
        public IActionResult Read(int id)
        {
            var post = _postService.GetPost(id);

            return View(post);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Read(PostToReadVm postWithComment)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var comment = postWithComment.NewComment;
            comment.ForumUserId = claims.Value.ToString();
            _postService.AddCommentToPost(comment);
            return RedirectToAction("Read", new { id = postWithComment.Id });
        }

        public IActionResult ReportPost(int postId)
        {
            _postService.ReportPost(postId);
            return RedirectToAction("Read", (new {id = postId }));
        }

        public IActionResult MyPosts()
        {
            var posts = _postService.GetUserPosts(User.FindFirstValue(ClaimTypes.NameIdentifier), 1,"");

            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(posts);
        }

        [HttpPost]
        public IActionResult MyPosts(int? page, string searchString)
        {
            if (searchString is null)
            {
                searchString = String.Empty;
            }
            if (!page.HasValue)
            {
                page = 1;
            }
            ViewBag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _postService.GetUserPosts(User.FindFirstValue(ClaimTypes.NameIdentifier), page.Value, searchString);

            return View(posts);
        }


        [HttpGet]
        public IActionResult EditPost(int postId)
        {
            if (!_postService.CheckAuthor(postId,User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return RedirectToAction("Index");
            }
            var post = _postService.GetPostForUpdate(postId);

            return View(post);
        }

        [HttpPost]
        public IActionResult EditPost(PostForUpdateVm post)
        {
            _postService.UpdatePost(post);
            return RedirectToAction("Read", (new { id = post.Id }));
        }

        public IActionResult EditParagraph(int postId,int paragraphId)
        {
            if (!_postService.CheckAuthor(postId, User.FindFirstValue(ClaimTypes.NameIdentifier)))
            {
                return RedirectToAction("Index");
            }
            if (!_postService.CheckParagraph(postId, paragraphId))
            {
                return RedirectToAction("Index");
            }

            var paragraph = _postService.GetParagraph(paragraphId);

            return View(paragraph);
        }

        [HttpPost]
        public IActionResult EditParagraph(ParagraphDetailsVm paragraph)
        {
            var postId =_postService.UpdateParagraph(paragraph);
            return RedirectToAction("EditPost", (new { postId = postId }));
        }

        public IActionResult AddParagraph(int postId)
        {
            var paragraph = new NewParagraphVm()
            {
                PostId = postId,
                
            };
            return View(paragraph);
        }

        [HttpPost]
        public IActionResult AddParagraph(NewParagraphVm paragraph)
        {
            _postService.AttachParagraphToPost(paragraph);
            return RedirectToAction("EditPost", (new { postId = paragraph.PostId }));
        }

        public IActionResult DeletePost(int postId)
        {
            _postService.DeletePost(postId);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteParagraph(int paragraphId, int postId)
        {
            _postService.DeleteParagraph(paragraphId);
            return RedirectToAction("EditPost", (new { postId = postId }));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
