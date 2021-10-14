using System;
using System.Collections.Generic;

#nullable disable

namespace SocialApi_Models.EfModels
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParentcommentNavigation = new HashSet<Comment>();
        }

        public int CommentId { get; set; }
        public int? FightId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Comment1 { get; set; }
        public int? Parentcomment { get; set; }

        public virtual Comment ParentcommentNavigation { get; set; }
        public virtual ICollection<Comment> InverseParentcommentNavigation { get; set; }
    }
}
