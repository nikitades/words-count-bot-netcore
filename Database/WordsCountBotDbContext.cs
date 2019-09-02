using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WordsCountBot.Models;

namespace WordsCountBot.Database
{
    public class WordsCountBotDbContext : DbContext
    {
        public WordsCountBotDbContext(DbContextOptions<WordsCountBotDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                return;
            }
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<WordUsedTimes> Usages { get; set; }
    }
}