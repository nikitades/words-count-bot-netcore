using CommandLine;

namespace TelegramBot.Cli
{
    public class Options
    {
        [Option('t', "token")]
        public string Token { get; set; }

        [Value(0)]
        public string FirstValue { get; set; }

        [Value(1)]
        public string SecondValue { get; set; }
    }
}