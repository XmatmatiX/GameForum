using GameForum.Application.Interface;
using GameForum.Application.ViewModels.MergedVm;
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
    [Authorize]
    public class AddController : Controller
    {
        private readonly IMergedService _mergedService;

        public AddController(IMergedService mergedService)
        {
            _mergedService = mergedService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var template = _mergedService.GetNewPostTemplate();
            return View(template);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(NewPostWithGenresVm newPost)
        {
            if (!ModelState.IsValid)
            {
                return View(newPost);
            }
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            newPost.Post.ForumUserId = claims.Value.ToString();
            var id = _mergedService.AddNewPost(newPost);
            return RedirectToAction("AddParagraph",new {postId = id });
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddParagraph(int postId, bool firstParagraph = true)
        {
            var paragraph = new NewParagraphVm() { PostId = postId, FirstParagraph = firstParagraph };
            ViewBag.added = 0;
            return View(paragraph);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddParagraph(NewParagraphVm newParagraph)
        {
            _mergedService.AddNewParagraph(newParagraph);
            ViewBag.added = 1;
            return (RedirectToAction("AddParagraph", new { postId = newParagraph.PostId, firstParagraph = false }));
        }
    }
}
