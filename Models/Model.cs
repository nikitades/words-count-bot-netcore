using System;
using WordsCountBot.Contracts;

namespace WordsCountBot.Models
{
    public class Model : IModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}