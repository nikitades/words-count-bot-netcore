using Microsoft.AspNetCore.Mvc;
using TelegramBot.Core.Database;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.Models;

namespace TelegramBot.Core.Controllers
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

            var messageText = "Le petit @sobaque kek_kek pek PEK !!!";
            var words = Word.GetWordsFromText(messageText);
            var chat = new Chat
            {
                TelegramID = 666,
                Name = "Some funny chat"
            };
            _wordsRepo.Create(words);
            _wordsRepo.GetContext().SaveChanges();

            _chatsRepo.Create(chat);
            _chatsRepo.GetContext().SaveChanges();

            _usagesRepo.IncrementLinks(words, chat);
            _usagesRepo.GetContext().SaveChanges();

            return Json(_usagesRepo.GetBy(usage => usage.ChatID == chat.ID).ToList());
        }

        [Route("/test")]
        public JsonResult Test()
        {
            var usages = _usagesRepo.GetByTelegramIdAndWordsList(-381262923, new string[] { "suka" });
            return Json("Ok");
        }
    }
}