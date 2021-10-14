using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialApi_Business.Interfaces;
using SocialApi_Data;
using SocialApi_Models.EfModels;
using SocialApi_Models.ViewModels;

namespace SocialApi_Business.Repositories {
    public class FriendRepo : IModelMapper<Friend, ViewFriend>, IFriendRepo {

        private readonly SocialDBContext _context;

        private FriendRepo(SocialDBContext context) {
            _context = context;
        }

        public Task<Friend> ViewToEF(ViewFriend view) {
            throw new NotImplementedException();
        }

        public ViewFriend EFToView(Friend ef) {
            ViewFriend vf = new ViewFriend() {
                FriendId = ef.FriendId,
                UserId = ef.UserId,
                FriendUserId = ef.FriendUserId
            };
            return vf;
        }

        public Task<List<ViewFriend>> GetFriendList() {
            throw new NotImplementedException();
        }

        public Task<ViewFriend> RemoveFriend(ViewFriend v) {
            throw new NotImplementedException();
        }

        public Task<ViewFriend> AddFriend(ViewFriend v) {
            throw new NotImplementedException();
        }
    }
}
