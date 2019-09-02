namespace WordsCountBot.Models
{
    public class WordUsedTimes : Model
    {
        public int WordID { get; set; }
        public int ChatID { get; set; }
        public int UsedTimes { get; set; }

        public Chat Chat { get; set; }
        public Word Word { get; set; }
    }
}