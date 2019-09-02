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
        private IWordsRepository<Word> _wordsRepo;
        private IChatsRepository<Chat> _chatsRepo;
        public DefaultController(
            IWordsRepository<Word> wordsRepo,
            IChatsRepository<Chat> chatsRepo
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
            return Json(new {
                Chats = chats,
                Words = words
            });
        }
    }
}