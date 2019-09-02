using System.Collections.Generic;

namespace WordsCountBot.Models
{
    public class Word : Model
    {
        public string Text { get; set; }
        public IList<WordUsedTimes> Usages { get; set; }
    }
}