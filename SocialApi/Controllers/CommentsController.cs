using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialApi_Business.Interfaces;
using SocialApi_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialApi.Controllers {
    public class CommentsController : Controller {

        private readonly ICommentRepo _commentRepo;

        public CommentsController(ICommentRepo cr)
        {
            _commentRepo = cr;
        }
        // GET: api/
        [HttpGet]
        public async Task<ActionResult<List<ViewComment>>> GetComment()
        {
            return await _commentRepo.CommentListAsync();
        }
        [HttpGet("{fightid}")]
        public async Task<List<ViewComment>> Get(int fightid)
        {
            return await _commentRepo.SpecificCommentAsync(fightid);
        }
        [HttpPost("post")]
        public async Task<ActionResult> Add( ViewComment vC)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _commentRepo.PostCommentAsync(vC);
            return Ok(new { Success = true });
        }
    }
}
