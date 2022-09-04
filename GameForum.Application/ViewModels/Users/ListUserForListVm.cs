using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application.ViewModels.Users
{
    public class ListUserForListVm
    {
        public List<UserForListVm> Users { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }
        public int CurrentPage { get; set; }
        public string SearchString { get; set; }
    }
}
