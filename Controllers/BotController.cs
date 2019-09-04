using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using WordsCountBot.Contracts;

namespace WordsCountBot.Controllers
{
    public class BotController : Controller
    {
        private ITelegramBot _bot;
        public BotController(ITelegramBot bot)
        {
            _bot = bot;
        }

        [Route("/bot/handler")]
        [HttpPost]
        public IActionResult Handle([FromBody]Update update)
        {
            _bot.HandleUpdate(update);
            return Ok("ok");
        }
    }
}