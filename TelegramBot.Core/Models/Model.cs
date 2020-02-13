using System;
using TelegramBot.Core.Contracts;

namespace TelegramBot.Core.Models
{
    public abstract class Model : IModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}