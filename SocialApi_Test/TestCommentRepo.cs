using Microsoft.EntityFrameworkCore;
using SocialApi_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialApi_Business.Repositories;
using SocialApi_Models.EfModels;
using Xunit;
using SocialApi_Models.ViewModels;

namespace SocialApi_Test {
    public class TestCommentRepo {

        public static DbContextOptions<SocialDBContext> options { get; set; } = new DbContextOptionsBuilder<SocialDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        private SocialDBContext _context = new SocialDBContext(options);
        private CommentRepo cr;

        public TestCommentRepo() {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            //Seeding the database
            Comment c1 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1984"),
                Date = DateTime.Now,
                Comment1 = "First",
                Parentcomment = null,
                UserName = "Blake"

            };


            Comment c2 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1984"),
                Date = DateTime.Now,
                Comment1 = "This fight is awesome!",
                Parentcomment = null,
                UserName = "Josh"

            };

            Comment c3 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1985"),
                Date = DateTime.Now,
                Comment1 = "Sup guys!",
                Parentcomment = null,
                UserName = "Davian"

            };
            Comment c4 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1983"),
                Date = DateTime.Now,
                Comment1 = "Nah man, this is not good!",
                Parentcomment = 2,
                UserName = "Blake"

            };

            Comment c5 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1986"),
                Date = DateTime.Now,
                Comment1 = "I`m just happy to be here.",
                Parentcomment = 2,
                UserName = "Marcus"
            };

            Comment c6 = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1984"),
                Date = DateTime.Now,
                Comment1 = "You are not correct.",
                Parentcomment = 4,
                UserName = "Josh"

            };

            Comment c7 = new Comment()
            {
                FightId = 2,
                UserId = new Guid(),
                Date = DateTime.Now,
                Comment1 = "Fight!",
                Parentcomment = null,
                UserName = "Davian"

            };

            _context.Comments.Add(c1);
            _context.Comments.Add(c2);
            _context.Comments.Add(c3);
            _context.Comments.Add(c4);
            _context.Comments.Add(c5);
            _context.Comments.Add(c6);
            _context.Comments.Add(c7);
            _context.SaveChanges();

            cr = new CommentRepo(_context);


        }
        //Methods that are tested

        //1. EFToView
        // public ViewComment EFToView(Comment ef)
        [Fact]
        public void TestEFCommentToViewComment()
        {
            Comment sut = new Comment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1984"),
                Date = DateTime.Now,
                Comment1 = "First",
                Parentcomment = null,
                UserName = "Blake"
            };

            ViewComment result = cr.EFToView(sut);

            Assert.True(result is ViewComment);
            Assert.Equal(sut.FightId, result.FightId);
            Assert.Equal(sut.UserId, result.UserId);
            Assert.Equal(sut.Date, result.Date);
            Assert.Equal(sut.Comment1, result.Comment1);
            Assert.Equal(sut.Parentcomment, result.Parentcomment);
            Assert.Equal(sut.UserName, result.UserName);


        }

        //2. viewToEF
        //public async Task<Comment> ViewToEF(ViewComment View)
        [Fact]
        public async void TestViewCommentToEFComment()
        {
            ViewComment sut = new ViewComment()
            {
                FightId = 1,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1984"),
                Date = DateTime.Now,
                Comment1 = "First",
                Parentcomment = null,
                UserName = "Blake"
            };
            Comment result = await cr.ViewToEF(sut);

            Assert.True(result is Comment);
            Assert.Equal(sut.FightId, result.FightId);
            Assert.Equal(sut.UserId, result.UserId);
            Assert.Equal(sut.Date, result.Date);
            Assert.Equal(sut.Comment1, result.Comment1);
            Assert.Equal(sut.Parentcomment, result.Parentcomment);
            Assert.Equal(sut.UserName, result.UserName);

        }




        //3. Test specific records recieved from a given fightID

        // public async Task<List<ViewComment>> SpecificCommentAsync(int fightId)
        [Theory]
        [InlineData(2)]
        public async void TestSpecificCommentAsync(int fightId)
        {
            List<ViewComment> result = await cr.SpecificCommentAsync(fightId);

            Assert.True(result is List<ViewComment>);
            Assert.Single(result);
        
        }



        //4. TestAllRecordRecieved

        //public async Task<List<ViewComment>> CommentListAsync()
        [Fact]
        public async void TestCommentListAsync()
        {
            List<ViewComment> result = await cr.CommentListAsync();
            Assert.True(result is List<ViewComment>);
            Assert.Equal(7, result.Count);
        
        }



        //5. Test adding a comment to a fight
        [Fact]
        //public async Task<ViewComment> PostCommentAsync(ViewComment vc)
        public async void PostCommentAsync()
        {
            ViewComment vc1 = new ViewComment()
            {
                FightId = 22,
                UserId = new Guid("f814ad2f-a55a-4272-8af1-1bb9190c1986"),
                Date = DateTime.Now,
                Comment1 = "Hello Fight!",
                Parentcomment = null,
                UserName = "Davian"
            };

            ViewComment vc2 = await cr.PostCommentAsync(vc1);

            Assert.Equal(vc2.FightId, vc1.FightId);
            Assert.Equal(vc2.UserId, vc1.UserId);
            Assert.Equal(vc2.Date, vc1.Date); ///need to be reviewed, causing a failing test 
            Assert.Equal(vc2.Comment1, vc1.Comment1);
            Assert.Equal(vc2.Parentcomment, vc1.Parentcomment);
            Assert.Equal(vc2.UserName, vc1.UserName);



        }

    } 
}
