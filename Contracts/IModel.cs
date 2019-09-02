using System;

namespace WordsCountBot.Contracts
{
    public interface IModel
    {
        int ID { get; set; }
        DateTime CreatedAt { get; set; }
    }
}