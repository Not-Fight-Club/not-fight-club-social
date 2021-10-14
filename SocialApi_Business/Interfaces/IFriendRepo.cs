using SocialApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApi_Business.Interfaces {
    public interface IFriendRepo {
        Task<List<ViewFriend>> GetFriendList();
        Task<ViewFriend> AddFriend(ViewFriend v);
        Task<ViewFriend> RemoveFriend(ViewFriend v);
    }
}
