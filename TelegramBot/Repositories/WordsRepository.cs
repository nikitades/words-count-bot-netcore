using System;
using System.Collections.Generic;
using System.Linq;
using WordsCountBot.Contracts;
using WordsCountBot.Database;
using WordsCountBot.Models;

namespace WordsCountBot.Repositories
{
    public class WordsRepository : IWordsRepository<Word, WordsCountBotDbContext>
    {
        private WordsCountBotDbContext _ctx;

        public WordsCountBotDbContext GetContext()
        {
            return _ctx;
        }

        public WordsRepository(WordsCountBotDbContext context)
        {
            _ctx = context;
        }

        public void Create(Word w)
        {
            throw new NotImplementedException();
        }

        public void Create(IEnumerable<string> incomingWords)
        {
            throw new NotImplementedException();
        }

        public void Create(IEnumerable<Word> incomingWords)
        {
            var existingWords = _ctx.Words
                .Select(word => Word.EscapeString(word.Text))
                .ToList()
                .Where(word => !String.IsNullOrEmpty(word))
                .ToList();

            incomingWords = incomingWords
                .Select(word =>
                {
                    word.Text = Word.EscapeString(word.Text);
                    return word;
                })
                .Where(word => !String.IsNullOrEmpty(word.Text))
                .ToList()
                .Where(incomingWord => existingWords.IndexOf(incomingWord.Text) == -1)
                .ToList();

            _ctx.Words.AddRange(incomingWords);
        }

        public void Delete(Word w)
        {
            _ctx.Words.Remove(w);
        }

        public void Delete(int ID)
        {
            var word = _ctx.Words.Where(word => word.ID == ID).SingleOrDefault();
            if (word != null)
            {
                _ctx.Words.Remove(word);
            }
        }

        public Word Get(Word w)
        {
            if (w.ID != 0)
            {
                return _ctx.Words.Where(word => word.ID == w.ID).SingleOrDefault();
            }
            else if (w.Text != "")
            {
                return _ctx.Words.Where(word => word.Text == w.Text).SingleOrDefault();
            }
            throw new ArgumentException("No searchable fields found");
        }

        public IEnumerable<Word> GetAll()
        {
            return _ctx.Words.ToList();
        }

        public IEnumerable<Word> GetBy(Func<Word, bool> predicate)
        {
            return _ctx.Words.Where(predicate).ToList();
        }

        public IEnumerable<Word> GetByID(IEnumerable<int> IDs)
        {
            return _ctx.Words.Where(word => IDs.Contains(word.ID)).ToList();
        }

        public IEnumerable<Word> GetByText(IEnumerable<string> sourceWords)
        {
            return _ctx.Words.Where(word => sourceWords.Contains(word.Text)).ToList();
        }

        public Word GetOne(int ID)
        {
            return _ctx.Words.Where(word => word.ID == ID).SingleOrDefault();
        }

        public void Update(Word t)
        {
            _ctx.Words.Update(t);
        }
    }
}