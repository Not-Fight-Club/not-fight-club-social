using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApi_Models.ViewModels
{
    public class ViewFriend
    {
        public int FriendId { get; set; }

        public Guid? UserId { get; set; }

        public Guid? FriendUserId { get; set; }
    }
}
