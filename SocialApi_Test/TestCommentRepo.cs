using Microsoft.EntityFrameworkCore;
using SocialApi_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialApi_Business.Repositories;

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
            cr = new CommentRepo(_context);
        }


    }
}
