using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordsCountBot.Models
{
    public class Word : Model
    {
        public string Text { get; set; }
        public IList<WordUsedTimes> Usages { get; set; }

        public static string EscapeString(string input)
        {
            return Regex.Replace(
                Regex.Replace(input, @"[^A-Za-zА-Яа-я-_]", " "),
                @"\W{2,}",
                " "
            ).Trim();
        }

        public static IEnumerable<Word> GetWordsFromText(string text)
        {
            var words = new List<Word>();
            foreach (var phrase in text.Split(" ")) {
                words.Add(new Word {
                    Text = phrase
                });
            }
            return words.Distinct();
        }

        public static IEnumerable<Word> FilterWordsList(IEnumerable<Word> list)
        {
            return list.Where(word => word.Text.Length > 3).ToList();
        }
    }
}