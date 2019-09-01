using Microsoft.AspNetCore.Mvc;
using WordsCountBot.Database;

namespace WordsCountBot.Controllers
{
    public class DefaultController : Controller
    {
        private WordsCountBotDbContext _context;
        public DefaultController(WordsCountBotDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public JsonResult Index()
        {
            return Json("kek");
        }
    }
}