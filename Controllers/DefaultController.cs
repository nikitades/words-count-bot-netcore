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
        private IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> _usagesRepo;
        public DefaultController(
            IWordsRepository<Word, WordsCountBotDbContext> wordsRepo,
            IChatsRepository<Chat, WordsCountBotDbContext> chatsRepo,
            IUsagesRepository<WordUsedTimes, WordsCountBotDbContext> usagesRepo
        )
        {
            _wordsRepo = wordsRepo;
            _chatsRepo = chatsRepo;
            _usagesRepo = usagesRepo;
        }

        [Route("/")]
        public JsonResult Index()
        {
            var words = _wordsRepo.GetAll();
            var chats = _chatsRepo.GetAll();
            var usages = _usagesRepo.GetAll();
            return Json(new
            {
                Chats = chats,
                Words = words,
                Usages = usages
            });
        }
    }
}