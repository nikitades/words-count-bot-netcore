using System;
using System.Collections.Generic;
using System.Linq;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Repositories
{
    public class UsagesRepository : IUsagesRepository<WordUsedTimes, WordsCountBotDbContext>
    {
        private WordsCountBotDbContext _ctx;

        public WordsCountBotDbContext GetContext()
        {
            return _ctx;
        }

        public UsagesRepository(WordsCountBotDbContext context)
        {
            _ctx = context;
        }

        public void Create(WordUsedTimes wut)
        {
            _ctx.Usages.Add(wut);
        }

        public void Delete(WordUsedTimes wut)
        {
            _ctx.Usages.Remove(wut);
        }

        public void Delete(int ID)
        {
            var wut = _ctx.Usages.Where(wut => wut.ID == ID).SingleOrDefault();
            if (wut != null)
            {
                _ctx.Usages.Remove(wut);
            }
        }

        public WordUsedTimes Get(WordUsedTimes wut)
        {
            return GetOne(wut.ID);
        }

        public IEnumerable<WordUsedTimes> GetAll()
        {
            return _ctx.Usages.ToList();
        }

        public IEnumerable<WordUsedTimes> GetBy(Func<WordUsedTimes, bool> predicate)
        {
            return _ctx.Usages.Where(predicate).ToList();
        }

        public WordUsedTimes GetOne(int ID)
        {
            return _ctx.Usages.Where(wut => wut.ID == ID).SingleOrDefault();
        }

        public void Update(WordUsedTimes wut)
        {
            _ctx.Usages.Update(wut);
        }

        public void IncrementLinks(IEnumerable<Word> words, Chat chat)
        {
            //TODO: написать собственный инсёрт, который бы джойнил по тексту слова и вставлял чат
        }
    }
}