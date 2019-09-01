using Microsoft.AspNetCore.Mvc;
using WordsCountBot.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var words = _context.Words
                .Where(word => word.Text == "kek")
                .Include(words => words.Usages)
                    .ThenInclude(usage => usage.Chat)
                .ToList();
            return Json(words);
        }
    }
}