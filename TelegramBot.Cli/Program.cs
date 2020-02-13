using System;
using CommandLine;
using TelegramBot.Cli.TelegramClientService;

namespace TelegramBot.Cli
{

    class Program
    {
        private static Mode _mode;
        private static Options _opts;
        private static ITelegramClientService _tgClientService = new Service();

        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Prepare(args);
            await RunAsync();
        }

        static void Prepare(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o => _opts = o);
            _mode = new Mode(_opts, _tgClientService);
        }

        static async System.Threading.Tasks.Task RunAsync()
        {
            var res = await _mode.Command.Execute();
            Console.WriteLine(res.Message);
        }
    }
}
