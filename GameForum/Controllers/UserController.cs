using GameForum.Application.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GameForum.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userService.GetUsers(1,"");
            //var claimIdentity = (ClaimsIdentity)User.Identity;
            //var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //if (claims != null)
            //{
            //    ViewData["userId"] = claims.Value.ToString();
            //}
            //else
            //{
            //    ViewData["userId"] = "";
            //}
            return View(users);
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

            var users = _userService.GetUsers(page.Value,searchString);
            return View(users);
        }

        public IActionResult UserDetails(string id)
        {

            var userDetails =_userService.GetUserDetails(id);

            return View(userDetails);
        }
    }
}
