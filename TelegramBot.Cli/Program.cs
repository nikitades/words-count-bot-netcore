using System;
using CommandLine;

namespace TelegramBot.Cli
{

    class Program
    {
        private static Mode _mode;
        private static Options _opts;

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Prepare(args);
            await RunAsync();
        }

        static void Prepare(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => _opts = o);
            _mode = new Mode(_opts);
        }

        static async System.Threading.Tasks.Task RunAsync()
        {
            await _mode.Command.Execute();
        }
    }
}
