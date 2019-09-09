using System.Collections.Generic;

namespace WordsCountBot.Models
{
    public class Chat : Model
    {
        public long TelegramID { get; set; }
        public string Name { get; set; }
        public IList<WordUsedTimes> Usages { get; set; }
    }
}