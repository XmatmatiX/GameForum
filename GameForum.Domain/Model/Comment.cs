using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public bool IsActive { get; set; }

        public string ForumUserId { get; set; }
        public virtual ForumUser Author { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }


    }
}
