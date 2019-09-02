using Microsoft.AspNetCore.Mvc;
using WordsCountBot.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WordsCountBot.Contracts;
using WordsCountBot.Models;

namespace WordsCountBot.Controllers
{
    public class DefaultController : Controller
    {
        private IWordsRepository<Word, WordsCountBotDbContext> _wordsRepo;
        private IChatsRepository<Chat, WordsCountBotDbContext> _chatsRepo;
        public DefaultController(
            IWordsRepository<Word, WordsCountBotDbContext> wordsRepo,
            IChatsRepository<Chat, WordsCountBotDbContext> chatsRepo
        )
        {
            _wordsRepo = wordsRepo;
            _chatsRepo = chatsRepo;
        }

        [Route("/")]
        public JsonResult Index()
        {
            var words = _wordsRepo.GetAll();
            var chats = _chatsRepo.GetAll();
            return Json(new
            {
                Chats = chats,
                Words = words
            });
        }
    }
}