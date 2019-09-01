using System.Collections.Generic;

namespace WordsCountBot.Models
{
    public class Chat
    {
        public int ID { get; set; }
        public int TelegramID { get; set; }
        public string Name { get; set; }
        public IList<WordUsedTimes> Usages { get; set; }
    }
}