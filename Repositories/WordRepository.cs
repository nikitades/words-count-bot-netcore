using System.Collections.Generic;
using Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace Repositories
{
    public class WordRepository : AbstractRepository<Word>, IRepository<Word>
    {
        private WordsCountBotDbContext _ctx;
        public WordRepository(WordsCountBotDbContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public override void Create(Word t)
        {
            _ctx.Words.Add(t);
            //TODO: выяснить, когда правильно вызывать .SaveChanges
        }

        public override void Delete(Word t)
        {
            throw new System.NotImplementedException();
        }

        public override Word Find(int i)
        {
            throw new System.NotImplementedException();
        }

        public override ICollection<Word> FindAll()
        {
            throw new System.NotImplementedException();
        }

        public override void Update(Word t)
        {
            throw new System.NotImplementedException();
        }
    }
}