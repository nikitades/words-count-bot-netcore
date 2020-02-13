using System;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Core.Contracts;

namespace TelegramBot.Core.Controllers
{
    [Route("bot")]
    public class BotController : Controller
    {
        private ITelegramBot _bot;
        public BotController(ITelegramBot bot)
        {
            _bot = bot;
        }

        [HttpPost("handler")]
        public IActionResult Handle([FromBody]Update update)
        {
            var botAction = _bot.HandleUpdate(update);
            _bot.ProcessAction(botAction);
            return Ok("ok");
        }

        [HttpGet("setWebhook")]
        public IActionResult SetWebhook()
        {
            try
            {
                _bot.SetWebhookAsync();
                return Ok("ok");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}