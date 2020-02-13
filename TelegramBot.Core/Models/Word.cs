using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace TelegramBot.Core.Models
{
    public class Word : Model
    {

        public class WordsEqualityComparer : IEqualityComparer<Word>
        {
            public bool Equals([AllowNull] Word x, [AllowNull] Word y)
            {
                return x.Text == y.Text;
            }

            public int GetHashCode([DisallowNull] Word obj)
            {
                return obj.Text.GetHashCode();
            }
        }

        public string Text { get; set; }
        public IList<WordUsedTimes> Usages { get; set; }

        [NotMapped]
        public bool TooShort { get; set; }

        public static string EscapeString(string input)
        {
            return Regex.Replace(
                Regex.Replace(input, @"[^A-Za-zА-Яа-я-_]", " "),
                @"\W{2,}",
                " "
            ).Trim().ToLower();
        }

        public static IEnumerable<Word> GetWordsFromText(string text)
        {
            var words = new List<Word>();
            var wordParts = text.Split(" ").Distinct();
            foreach (var phrase in wordParts)
            {
                words.Add(new Word
                {
                    Text = phrase,
                    CreatedAt = DateTime.Now,
                    TooShort = phrase.Length < 3
                });
            }
            return words;
        }
    }
}