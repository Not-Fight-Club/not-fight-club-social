using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApi_Models.ViewModels
{
    public class ViewComment
    {
        public int CommentId { get; set; }
        public int? FightId { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public DateTime? Date { get; set; }

        public string Comment1 { get; set; }

        public int? Parentcomment { get; set; }

        public List<ViewComment> Replies { get; set; }


    }
}
