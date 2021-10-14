using Microsoft.EntityFrameworkCore;
using SocialApi_Business.Repositories;
using SocialApi_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApi_Test {
    public class TestFriendRepo {
        public static DbContextOptions<SocialDBContext> options { get; set; } = new DbContextOptionsBuilder<SocialDBContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        private SocialDBContext _context = new SocialDBContext(options);
        private FriendRepo cr;

        public TestFriendRepo() {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            //Seeding the database
            cr = new FriendRepo(_context);
        }
    }
}
