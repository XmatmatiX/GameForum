using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Posts
{
    public class ListUserPostForListVm
    {
        public List<UserPostForListVm> PostList { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
    }
}
