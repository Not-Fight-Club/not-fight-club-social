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
    class CommentRepo : IModelMapper<Comment, ViewComment>, ICommentRepo
    {
        private readonly SocialDBContext _context;

        private CommentRepo(SocialDBContext context) {
            _context = context;
        }//end of constructor


        //implement Imodel Mapper interface 1/2
        public ViewComment EFToView(Comment ef)
        {
            ViewComment vc = new ViewComment();
            vc.CommentId = ef.CommentId;
            vc.FightId = ef.FightId;
            vc.UserId = ef.UserId;
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
                where o.CommentId == View.CommentId && o.FightId == View.FightId && o.UserId == View.UserId && o.Date == View.Date && o.Comment1 == View.Comment1 && o.Parentcomment == View.Parentcomment 
                select new { o }).ToListAsync();

            foreach (var x in await allcommentsquery)
            {
                c.CommentId = x.o.CommentId;
                c.FightId = x.o.FightId;
                c.UserId = x.o.UserId;
                c.Date = x.o.Date;
                c.Comment1 = x.o.Comment1;
                c.Parentcomment = x.o.Parentcomment;
            }

            return c;
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
                vc.Parentcomment = x.o.Parentcomment;
                vcl.Add(vc);
            }
            return vcl;
        }


       
    }
}
