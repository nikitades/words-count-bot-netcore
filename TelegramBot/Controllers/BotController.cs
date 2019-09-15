using System;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WordsCountBot.Contracts;

namespace WordsCountBot.Controllers
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
                _bot.SetWebhook();
                return Ok("ok");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }
    }
}