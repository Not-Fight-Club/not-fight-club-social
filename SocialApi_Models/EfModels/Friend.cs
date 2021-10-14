using System;
using System.Collections.Generic;

#nullable disable

namespace SocialApi_Models.EfModels
{
    public partial class Friend
    {
        public int FriendId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? FriendUserId { get; set; }
    }
}
