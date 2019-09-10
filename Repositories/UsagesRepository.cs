using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            var wordsReplaces = new List<string>();
            for (var i = 0; i < words.Count(); i++)
            {
                wordsReplaces.Add($"@Word{i}");
            }
            var inList = String.Join(", ", wordsReplaces);
            var sqlStr = $@"INSERT INTO 
                            public.""Usages"" (""WordID"", ""ChatID"", ""UsedTimes"", ""CreatedAt"")
                            
                            (SELECT 
                                w.""ID"", 
                                (SELECT c.""ID"" FROM ""Chats"" c WHERE c.""TelegramID"" = @TelegramChatID), 
                                1, @CurTime 
                                FROM ""Words"" w WHERE w.""Text"" IN (
                                    {inList}
                                )
                            )
                        ON CONFLICT (""WordID"", ""ChatID"") DO UPDATE SET ""UsedTimes"" = public.""Usages"".""UsedTimes"" + 1        
            ";

            var sqlParameters = new List<Object>();
            sqlParameters.Add(new NpgsqlParameter("CurTime", DateTime.Now));
            sqlParameters.Add(new NpgsqlParameter("TelegramChatID", chat.TelegramID));
            var wordsList = words.ToList();
            for (var i = 0; i < words.Count(); i++)
            {
                sqlParameters.Add(new NpgsqlParameter($"@Word{i}", wordsList[i].Text));
            }
            _ctx.Database.ExecuteSqlRaw(sqlStr, sqlParameters);
        }

        public IEnumerable<WordUsedTimes> GetByTelegramIdAndWordsList(long telegramId, IEnumerable<string> wordsList)
        {
            if (wordsList.Count() == 0)
            {
                return new List<WordUsedTimes>();
            }
            var wordsReplaces = wordsList.Select((word, i) => $"@WordRep{i}").ToList();
            var sqlParameters = wordsList.Select((word, i) => new NpgsqlParameter($"WordRep{i}", word)).ToList();
            sqlParameters.Add(new NpgsqlParameter("ChatTelegramId", telegramId));

            var usages = _ctx.Usages
                .FromSqlRaw($@"SELECT u.*
                    FROM public.""Usages"" u
                        LEFT JOIN public.""Chats"" c ON c.""ID"" = u.""ChatID""
                        LEFT JOIN public.""Words"" w ON w.""ID"" = u.""WordID""
                        WHERE c.""TelegramID"" = @ChatTelegramId
                        AND w.""Text"" IN ({String.Join(", ", wordsReplaces)})",
                        sqlParameters.ToArray());
            return usages.ToList();
        }

        public IEnumerable<WordUsedTimes> GetByTelegramIdTopWords(long telegramId, int limit)
        {
            var sqlParameters = new NpgsqlParameter[]{
                new NpgsqlParameter("ChatTelegramId", telegramId),
                new NpgsqlParameter("Limit", limit)
            };

            var usages = _ctx.Usages
                .FromSqlRaw($@"SELECT u.*
                    FROM public.""Usages"" u
                        LEFT JOIN public.""Chats"" c ON c.""ID"" = u.""ChatID""
                        LEFT JOIN public.""Words"" w ON w.""ID"" = u.""WordID""
                        WHERE c.""TelegramID"" = @ChatTelegramId
                        ORDER BY u.""UsedTimes"" DESC
                        LIMIT @Limit
                ", sqlParameters.ToArray());
            return usages.ToList();
        }
    }
}