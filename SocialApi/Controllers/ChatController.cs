using Microsoft.AspNetCore.Mvc;
using PusherServer;
using SocialApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotFightClub_WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ChatController : Controller
    {
        [HttpPost("messages")]
        public async Task<ActionResult> Message(MessageDTO dto)
        {
            var options = new PusherOptions
            {
                Cluster = "us2",
                Encrypted = true
            };

            var pusher = new Pusher(
              "1271188",
              "b23323ca0f6cc9730893",
              "cba464291f0bdbb746e2",
              options);

            await pusher.TriggerAsync(
              "not-fight-chat",
              "message",
              new
              {
                  username = dto.username,
                  message = dto.message
              });
            return Ok(new string[] { });
        }
    }
}
