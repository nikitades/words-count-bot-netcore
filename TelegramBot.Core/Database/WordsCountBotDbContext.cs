using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TelegramBot.Core.Models;

namespace TelegramBot.Core.Database
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordUsedTimes>()
            .HasIndex(wut => new { wut.WordID, wut.ChatID }).IsUnique();
            modelBuilder.Entity<Word>()
            .HasIndex(word => new { word.Text }).IsUnique();
            modelBuilder.Entity<Chat>()
            .HasIndex(chat => new { chat.TelegramID }).IsUnique();
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<WordUsedTimes> Usages { get; set; }
    }
}