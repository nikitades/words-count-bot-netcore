using System.Linq;
using System;
using TelegramBot.Cli.Commands;
using ICommand = TelegramBot.Cli.Commands.ICommand;

namespace TelegramBot.Cli
{
    //A class to accumulate all the input data
    class Mode
    {
        internal ICommand Command { get; set; }

        private readonly Type[] _commandsAvailable = new Type[]
        {
            typeof(SetWebhookCommand),
            typeof(GetWebhookCommand)
        };

        public Mode(Options opts)
        {
            if (String.IsNullOrWhiteSpace(opts.FirstValue)) throw new ArgumentException("No command name given!");
            var commandCode = opts.FirstValue;
            var cmdType = _commandsAvailable.FirstOrDefault(type => type.Name == commandCode + "Command");
            if (cmdType == null) throw new ArgumentException("Bad command code given!");
            object[] ctorArgs = new object[] {
                opts
            };
            Command = (ICommand)Activator.CreateInstance(cmdType, ctorArgs);
        }
    }
}