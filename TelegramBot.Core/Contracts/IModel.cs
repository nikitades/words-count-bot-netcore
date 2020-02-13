using System;

namespace TelegramBot.Core.Contracts
{
    public interface IModel
    {
        int ID { get; set; }
        DateTime CreatedAt { get; set; }
    }
}