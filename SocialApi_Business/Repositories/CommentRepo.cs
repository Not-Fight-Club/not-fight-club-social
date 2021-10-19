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

namespace SocialApi_Business.Repositories
{
    public class CommentRepo : IModelMapper<Comment, ViewComment>, ICommentRepo
    {
        private readonly SocialDBContext _context;

        public CommentRepo(SocialDBContext context) {
            _context = context;
        }//end of constructor

        //implement Imodel Mapper interface 1/2
        public ViewComment EFToView(Comment ef)
        {
            ViewComment vc = new ViewComment();
            vc.CommentId = ef.CommentId;
            vc.FightId = ef.FightId;
            vc.UserId = ef.UserId;
            vc.UserName = ef.UserName;
            vc.Date = ef.Date;
            vc.Comment1 = ef.Comment1;
            vc.Parentcomment = ef.Parentcomment;

            return vc;
        }

        //implement Imodel Mapper interface 2/2
        public async Task<Comment> ViewToEF(ViewComment View)
            {
             
            Comment c = new Comment();

            var allcommentsquery = (from o in _context.Comments 
                where o.CommentId == View.CommentId && o.UserName == View.UserName && o.FightId == View.FightId && o.UserId == View.UserId && o.Date == View.Date && o.Comment1 == View.Comment1 && o.Parentcomment == View.Parentcomment 
                select new { o }).ToListAsync();

            foreach (var x in await allcommentsquery)
            {
                c.CommentId = x.o.CommentId;
                c.FightId = x.o.FightId;
                c.UserId = x.o.UserId;
                c.Date = x.o.Date;
                c.Comment1 = x.o.Comment1;
                c.UserName = x.o.UserName;
                c.Parentcomment = x.o.Parentcomment;
            }

            return c;
            }

        //Thanks Jon
        public async Task<List<ViewComment>> SpecificCommentAsync(int fightId) {
            List<Comment> allRelatedComments = await _context.Comments.Where(c => c.FightId == fightId).ToListAsync();
            Dictionary<int, ViewComment> viewModelLookup = new Dictionary<int, ViewComment>();
            List<ViewComment> results = new List<ViewComment>();

            foreach (var comment in allRelatedComments) {
                ViewComment newCommentModel = new ViewComment {
                    CommentId = comment.CommentId,
                    FightId = comment.FightId,
                    UserId = comment.UserId,
                    UserName = comment.UserName,
                    Date = comment.Date,
                    Comment1 = comment.Comment1,
                    Parentcomment = comment.Parentcomment,
                Replies = new List<ViewComment>(),
                };

                viewModelLookup.Add(comment.CommentId, newCommentModel);

                if (comment.Parentcomment == null)
                    results.Add(newCommentModel);
            }

            foreach (var comment in allRelatedComments) {
                if (comment.Parentcomment != null) {
                    viewModelLookup[comment.Parentcomment.Value].Replies.Add(viewModelLookup[comment.CommentId]);
                }
            }
            return results;
        }

        //implement ICommentRepo interface
        public async Task<List<ViewComment>> CommentListAsync()
        {
            ViewComment vc = new ViewComment();
            List<ViewComment> vcl = new List<ViewComment>();

            var commentsquery = (from o in _context.Comments select new { o }).ToListAsync();

            foreach (var x in await commentsquery)
            {
                //vc = new ViewComment();

                vc.CommentId = x.o.CommentId;
                vc.FightId = x.o.FightId;
                vc.UserId = x.o.UserId;
                vc.Date = x.o.Date;
                vc.Comment1 = x.o.Comment1;
                vc.UserName = x.o.UserName;
                vc.Parentcomment = x.o.Parentcomment;
                vcl.Add(vc);
            }
            return vcl;
        }

        public async Task<ViewComment> PostCommentAsync(ViewComment vc) {
            Comment c = new Comment() {
                FightId = vc.FightId,
                UserId = vc.UserId,
                Date = vc.Date,
                Comment1 = vc.Comment1,
                Parentcomment = vc.Parentcomment,
                UserName = vc.UserName
            };
            _context.Comments.Add(c);
            await _context.SaveChangesAsync();
            return EFToView(c);
        }       
    }
}
