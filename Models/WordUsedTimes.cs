namespace WordsCountBot.Models
{
    public class WordUsedTimes
    {
        public int ID { get; set; }
        public int WordID { get; set; }
        public int ChatID { get; set; }
        public int UsedTimes { get; set; }

        public Chat Chat { get; set; }
        public Word Word { get; set; }
    }
}