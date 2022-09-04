using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Domain.Model
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isActive { get; set; }
        public bool IsReported { get; set; }
        public bool IsChecked { get; set; }
        public bool IsBanned { get; set; }
        public DateTime CreateTime { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        public string ForumUserId { get; set; }
        public virtual ForumUser Author { get; set; }

        public virtual ICollection<Paragraph> Paragraphs { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
