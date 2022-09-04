using GameForum.Application.Interface;
using GameForum.Application.ViewModels.Genres;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameForum.Web.Controllers
{
    [Authorize(Roles ="Employee")]
    public class AdminController : Controller
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdminController(IPostService postService, IUserService userService, IRoleService roleService)
        {
            _postService = postService;
            _userService = userService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles ="Manager")]
        public IActionResult Roles()
        {
            var roles = _roleService.GetRoles();
            return View(roles);
        }

        [HttpPost]
        [Authorize(Roles ="Manager")]
        public IActionResult Roles(string newRole)
        {
            _roleService.AddNewRole(newRole);

            return RedirectToAction("Roles");
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult DeleteRole(string roleId)
        {
            _roleService.DeleteRole(roleId);
            return RedirectToAction("Roles");
        }

        [HttpGet]
        [Authorize(Roles ="Manager")]
        public IActionResult AddRole(string roleId)
        {
            var users = _userService.GetRoleUsers(roleId, 1, "", false);

            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AddRole(string roleId, int? page, string searchString)
        {
            if (searchString is null)
            {
                searchString = "";
            }
            if (!page.HasValue)
            {
                page = 1;
            }

            var users = _userService.GetRoleUsers(roleId, page.Value, searchString, false);
            return View(users);
        }

        [HttpGet]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteUserRole(string roleId)
        {
            var users = _userService.GetRoleUsers(roleId, 1, "", true);

            return View(users);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteUserRole(string roleId, int? page, string searchString)
        {
            if (searchString is null)
            {
                searchString = "";
            }
            if (!page.HasValue)
            {
                page = 1;
            }

            var users = _userService.GetRoleUsers(roleId, page.Value, searchString, false);

            return View(users);
        }

        [Authorize(Roles = "Manager")]
        public IActionResult AttachRole(string userId, string roleId)
        {
            _roleService.AttachRole(userId, roleId);
            return RedirectToAction("Roles");
        }

        [Authorize(Roles = "Manager")]
        public IActionResult DetachRole(string userId, string roleId)
        {
            _roleService.DetachRole(userId, roleId);
            return RedirectToAction("Roles");
        }



        [HttpGet]
        [Authorize(Roles = "Moderator")]
        public IActionResult ReportedPosts()
        {
            var posts = _postService.GetReportedPosts(1);

            return View(posts);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator")]
        public IActionResult ReportedPosts(int? page)
        {
            if (!page.HasValue)
            {
                page = 1;
            }

            var posts = _postService.GetReportedPosts(page.Value);
            return View(posts);
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult CheckPost(int postId)
        {
            var post = _postService.GetPost(postId);

            return View(post);
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult BanPost(int postId, bool ban)
        {
            if (ban)
            {
                _postService.BanPost(postId);
            }
            else
            {
                _postService.CheckPost(postId);
            }
            return RedirectToAction("ReportedPosts");
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult Genres()
        {
            var genres = _postService.GetGenresList();

            return View(genres);
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult AddGenre(string name)
        {
            _postService.AddNewGenre(name);

            return RedirectToAction("Genres");
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult DeleteGenre(int id)
        {
            _postService.DeleteGenre(id);

            return RedirectToAction("Genres");
        }

        [Authorize(Roles = "Moderator")]
        public IActionResult EditGenre(int id)
        {
            var genre = _postService.GetGenre(id);

            return View(genre);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public IActionResult EditGenre(GenreForListVm genre)
        {
            _postService.UpdateGenre(genre);

            return RedirectToAction("Genres");
        }

    }
}
