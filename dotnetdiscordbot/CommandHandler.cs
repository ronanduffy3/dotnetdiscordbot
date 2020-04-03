using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using System;
using Discord.WebSocket;
using pringles1_0.Modules.Public;

namespace pringles1_0
{
    class CommandHandler
    {
        private CommandService commands;
        private DiscordSocketClient client;
        private IDependencyMap map;

        public async Task Install(IDependencyMap _map)
        {
            // Creating the command service
            client = _map.Get<DiscordSocketClient>();
            commands = new CommandService();
            _map.Add(commands);
            map = _map;

            try
            {
                await commands.AddModulesAsync(Assembly.GetEntryAssembly());
            }
            catch
            {
                Console.WriteLine("It didn't work... Clearly");
            }

            client.MessageReceived += HandleCommand;
        }

        public async Task HandleCommand(SocketMessage parameterMessage)
        {
            

            // Don't handle the message if its a system message or if its empty
            var message = parameterMessage as SocketUserMessage;
            if (message == null) return;

            // Determine where the prefix ends
            int argPos = 0;
            // See if the message's prefix is valid
            if (!(message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.HasCharPrefix('!', ref argPos))) return;

            // Create a context
            var context = new SocketCommandContext(client, message);
            // Use the command and keep the result
            var result = await commands.ExecuteAsync(context, argPos, map);

            // Set a command failure message 
            if (!result.IsSuccess)
                await message.Channel.SendMessageAsync($"**Error:** `{result.ErrorReason}`");
        }
    }
}
