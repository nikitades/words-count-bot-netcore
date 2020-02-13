using System;
using System.Collections.Generic;
using System.Linq;
using TelegramBot.Core.Contracts;
using TelegramBot.Core.Database;
using TelegramBot.Core.Models;

namespace TelegramBot.Core.Repositories
{
    public class ChatsRepository : IChatsRepository<Chat, WordsCountBotDbContext>
    {
        private WordsCountBotDbContext _ctx;

        public WordsCountBotDbContext GetContext()
        {
            return _ctx;
        }

        public ChatsRepository(WordsCountBotDbContext context)
        {
            _ctx = context;
        }

        public void Create(Chat c)
        {
            var existingChat = _ctx.Chats.Where(chat => chat.TelegramID == c.TelegramID).SingleOrDefault();
            if (existingChat == null)
            {
                _ctx.Chats.Add(c);
            }
        }

        public void Delete(Chat t)
        {
            _ctx.Chats.Remove(t);
        }

        public void Delete(int ID)
        {
            Chat chat = _ctx.Chats.Where(_chat => _chat.ID == ID).SingleOrDefault();
            if (chat != null)
            {
                _ctx.Chats.Remove(chat);
            }
        }

        public Chat Get(Chat c)
        {
            if (c.ID != 0)
            {
                return _ctx.Chats.Where(chat => chat.ID == c.ID).SingleOrDefault();
            }
            else if (c.Name != "")
            {
                return _ctx.Chats.Where(chat => chat.Name == c.Name).SingleOrDefault();
            }
            else if (c.TelegramID != 0)
            {
                return _ctx.Chats.Where(chat => chat.TelegramID == c.TelegramID).SingleOrDefault();
            }
            throw new ArgumentException("No searchable fields found");
        }

        public IEnumerable<Chat> GetAll()
        {
            return _ctx.Chats.ToList();
        }

        public IEnumerable<Chat> GetBy(Func<Chat, bool> predicate)
        {
            return _ctx.Chats.Where(predicate).ToList();
        }

        public Chat GetOne(int ID)
        {
            return _ctx.Chats.Where(chat => chat.ID == ID).SingleOrDefault();
        }

        public void Update(Chat c)
        {
            _ctx.Chats.Update(c);
        }
    }
}